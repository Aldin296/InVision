using InVision_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InVision_API.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(
        IOptions<InVisionDatabaseSettings> InVisionDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            InVisionDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            InVisionDatabaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(
            InVisionDatabaseSettings.Value.CollectionName);
    }

    //GetAll
    public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();
    //Anstatt void geb ich task zurück

    //GetById
    public async Task<User?> GetAsync(string id) =>
        await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    //Create User
    public async Task CreateAsync(User newUser)
    {
        // Falls der Benutzer bzw ein Benutzer mit dieser Email bereits existiert
        var existingUser = await _userCollection.Find(x => x.Email == newUser.Email).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            // Fehlermeldung wird zurückgegeben
            throw new Exception("User with the same email already exists.");
        }

        await _userCollection.InsertOneAsync(newUser);
    }

    //Update User
    public async Task UpdateAsync(string id, User updatedUser) =>
        await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    //Delete User
    public async Task RemoveAsync(string id) =>
        await _userCollection.DeleteOneAsync(x => x.Id == id);

    //Login User
    public async Task<User?> LoginAsync(string email, string password)
    {
        // Find the user with the provided email
        var user = await _userCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        if (user != null)
        {
            // Validate the password (you might want to use a more secure password hashing method)
            if (user.Password == password)
            {
                // Password is correct, return the user
                return user;
            }
            else
            {
                // Password is incorrect, handle accordingly (e.g., return an error).
                throw new Exception("Incorrect password.");
            }
        }
        else
        {
            // User with the provided email not found, handle accordingly (e.g., return an error).
            throw new Exception("User not found.");
        }
    }

}