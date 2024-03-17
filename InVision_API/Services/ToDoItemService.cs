using InVision_API.Models;
using Microsoft.Extensions.Options;
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


        public async Task UpdateItemAsync(string itemId, TodoItem updatedItem)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(x => x.Id, itemId),
                Builders<User>.Filter.Eq("Items._id", updatedItem)
            );

            var update = Builders<User>.Update
                .Set("ToDoItems.$.Title", updatedItem.Title)
                .Set("ToDoItems.$.Description", updatedItem.Description)
                .Set("ToDoItems.$.State", updatedItem.State);


            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteItemAsync(string userId, string kboardId, string itemId)
        {
            var filter = Builders<User>.Filter.And(
            Builders<User>.Filter.Eq(x => x.Id, userId),
            Builders<User>.Filter.Eq("KBoards._id", kboardId)
        );

            var update = Builders<User>.Update.PullFilter("Items", Builders<TodoItem>.Filter.Eq("_id",itemId));

            await _userCollection.UpdateOneAsync(filter, update);
        }
    }
    
}

