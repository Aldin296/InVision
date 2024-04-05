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
		await _userCollection.Find(x => true).ToListAsync();


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
	// Update User
	public async Task UpdateAsync(string id, User updatedUser)
	{
		var filter = Builders<User>.Filter.Eq(x => x.Id, id);
		var updateBuilder = Builders<User>.Update;

		var updateDefinition = updateBuilder.Combine(
			// Dynamically add update operations for non-null properties
			updatedUser.Name != null ? updateBuilder.Set(x => x.Name, updatedUser.Name) : null,
			updatedUser.Password != null ? updateBuilder.Set(x => x.Password, updatedUser.Password) : null,
			updatedUser.Email != null ? updateBuilder.Set(x => x.Email, updatedUser.Email) : null,
			updatedUser.Notes != null ? updateBuilder.Set(x => x.Notes, updatedUser.Notes) : null,
			updatedUser.KBoards != null ? updateBuilder.Set(x => x.KBoards, updatedUser.KBoards) : null,
			updatedUser.Appointments != null ? updateBuilder.Set(x => x.Appointments, updatedUser.Appointments) : null,
			updatedUser.ProfilePicture != null ? updateBuilder.Set(x => x.ProfilePicture, updatedUser.ProfilePicture) : null,
			updatedUser.Notification != null ? updateBuilder.Set(x => x.Notification, updatedUser.Notification) : null,
			updatedUser.Role != null ? updateBuilder.Set(x => x.Role, updatedUser.Role) : null,
			updatedUser.salt != null ? updateBuilder.Set(x => x.salt, updatedUser.salt) : null
		);

		await _userCollection.UpdateOneAsync(filter, updateDefinition);
	}

	//Delete User
	public async Task RemoveAsync(string id) =>
		await _userCollection.DeleteOneAsync(x => x.Id == id);



}