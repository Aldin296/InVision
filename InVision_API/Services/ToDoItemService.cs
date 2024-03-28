using InVision_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;


namespace InVision_API.Services
{
    public class ToDoItemService
    {
        private readonly IMongoCollection<User> _userCollection;


        public ToDoItemService(IOptions<InVisionDatabaseSettings> InVisionDatabaseSettings)
        {
            var mongoClient = new MongoClient(InVisionDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(InVisionDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(InVisionDatabaseSettings.Value.CollectionName);
        }


        public async Task<List<TodoItem>> GetAllToDoItemsAsync(string userId, string kBoardId)
        {
            var user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();
            var kBoard = user?.KBoards.FirstOrDefault(k => k.Id == kBoardId);

            return kBoard?.Items ?? new List<TodoItem>();
        }

        public async Task CreateToDoItemAsync(string userId, string kboardId, TodoItem todoItem)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(x => x.Id, userId),
                Builders<User>.Filter.Eq("KBoards._id", kboardId)
            );

            var update = Builders<User>.Update.Push("KBoards.$.Items", todoItem);

            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateItemAsync(string userId, string kboardId, string itemId, TodoItem updatedItem)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(x => x.Id, userId),
                Builders<User>.Filter.ElemMatch(x => x.KBoards, kb => kb.Id == kboardId), // Filter KBoard by ID
                Builders<User>.Filter.Eq("KBoards.Items._id", itemId) // Filter item within the KBoard
            );

            var update = Builders<User>.Update
                .Set("KBoards.$.Items.$[item].Title", updatedItem.Title) // Update Title
                .Set("KBoards.$.Items.$[item].Description", updatedItem.Description) // Update Description
                .Set("KBoards.$.Items.$[item].State", updatedItem.State); // Update State

            var options = new UpdateOptions { ArrayFilters = new List<ArrayFilterDefinition> { new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("item._id", itemId)) } };

            await _userCollection.UpdateOneAsync(filter, update, options);
        }


        public async Task DeleteItemAsync(string userId, string kboardId, string itemId)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(x => x.Id, userId),
                Builders<User>.Filter.ElemMatch(x => x.KBoards, kb => kb.Id == kboardId), // Filter KBoard by ID
                Builders<User>.Filter.Eq("KBoards.Items._id", itemId) // Filter item within the KBoard
            );

            var update = Builders<User>.Update.PullFilter("KBoards.$.Items", Builders<TodoItem>.Filter.Eq("_id", itemId));

            await _userCollection.UpdateOneAsync(filter, update);
        }

    }

}

