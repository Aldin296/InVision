using InVision_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InVision_API.Services
{
    public class NoteService
    {
        private readonly IMongoCollection<User> _userCollection;

        public NoteService(IOptions<InVisionDatabaseSettings> InVisionDatabaseSettings)
        {
            var mongoClient = new MongoClient(InVisionDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(InVisionDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(InVisionDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<Note>> GetNotesAsync(string userId)
        {
            var user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();

            return user?.Notes ?? new List<Note>();
        }

        public async Task<Note?> GetNoteAsync(string userId, string noteId)
        {
            var user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();

            return user?.Notes?.Find(note => note.Id == noteId);
        }

        public async Task CreateNoteAsync(string userId, Note newNote)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, userId);
            var update = Builders<User>.Update.Push(x => x.Notes, newNote);

            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateNoteAsync(string userId, string noteId, Note updatedNote)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(x => x.Id, userId),
                Builders<User>.Filter.Eq("Notes._id", noteId)
            );

            var update = Builders<User>.Update
                .Set("Notes.$.Title", updatedNote.Title)
                .Set("Notes.$.Content", updatedNote.Content)
                .Set("Notes.$.IsDone", updatedNote.IsDone);

            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteNoteAsync(string userId, string noteId)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, userId);
            var update = Builders<User>.Update.PullFilter("Notes", Builders<Note>.Filter.Eq("_id", noteId));

            await _userCollection.UpdateOneAsync(filter, update);
        }
    }
}
