using InVision_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace InVision_API.Services
{
    public class ToDoItemService
    {
       /* public KBoardService boardService;

        
        
        private readonly IMongoCollection<User> _userCollection;

        public ToDoItemService(IOptions<InVisionDatabaseSettings> InVisionDatabaseSettings)
        {
            boardService.GetAllKBoardsAsync();
            var mongoClient = new MongoClient(InVisionDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(InVisionDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(InVisionDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<KBoard>> GetAllKBoardsAsync(string userId)
        {
            var user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();

            return user?.KBoards ?? new List<KBoard>();
        }

        public async Task<KBoard?> GetKBoardAsync(string userId, string kboardId)
        {
            var user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();

            return user?.KBoards?.Find(kboard => kboard.Id == kboardId);
        }

        public async Task CreateToDoItemAsync(string userId, string kboardId, TodoItem todoItem)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, userId);
            var update = Builders<User>.Update.Push(x => x.KBoards, newKBoard);

            await _userCollection.UpdateOneAsync(filter, update);
        }

        /*public async Task UpdateToDoAsync(string kboardId, TodoItem updatedItem)
        {
            var filter = Builders<KBoard>.Filter.And(
                Builders<KBoard>.Filter.Eq(x => x.Id, kboardId),
                Builders<KBoard>.Filter.Eq("Items._id", ToDoItemId)
            );

            var update = Builders<User>.Update
                .Set("ToDoItems.$.Title", updatedKBoard.Name)
                .Set("ToDoItems.$.Content", updatedKBoard.Description);


            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteKBoardAsync(string userId, string KBoardId)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, userId);
            var update = Builders<User>.Update.PullFilter("KBoards", Builders<KBoard>.Filter.Eq("_id", KBoardId));

            await _userCollection.UpdateOneAsync(filter, update);
        }
    }
    */
}
}
