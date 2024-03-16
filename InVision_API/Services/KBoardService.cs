using InVision_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InVision_API.Services
{
	public class KBoardService
	{
		private readonly IMongoCollection<User> _userCollection;

		public KBoardService(IOptions<InVisionDatabaseSettings> InVisionDatabaseSettings)
		{
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

		public async Task CreateKBoardAsync(string userId, KBoard newKBoard)
		{
			var filter = Builders<User>.Filter.Eq(x => x.Id, userId);
			var update = Builders<User>.Update.Push(x => x.KBoards, newKBoard);

			await _userCollection.UpdateOneAsync(filter, update);
		}

		public async Task UpdateKboardAsync(string userId, string kboardId, KBoard updatedKBoard)
		{
			var filter = Builders<User>.Filter.And(
				Builders<User>.Filter.Eq(x => x.Id, userId),
				Builders<User>.Filter.Eq("KBoards._id", kboardId)
			);

			var update = Builders<User>.Update
				.Set("KBoards.$.Title", updatedKBoard.Name)
				.Set("KBoards.$.Content", updatedKBoard.Description);
				

			await _userCollection.UpdateOneAsync(filter, update);
		}

		public async Task DeleteKBoardAsync(string userId, string KBoardId)
		{
			var filter = Builders<User>.Filter.And(
				Builders<User>.Filter.Eq(x => x.Id, userId),
				Builders<User>.Filter.ElemMatch(x => x.KBoards, kb => kb.Id == KBoardId)
			);

			var update = Builders<User>.Update.PullFilter(x => x.KBoards, kb => kb.Id == KBoardId);

			await _userCollection.UpdateOneAsync(filter, update);
		}

	}
}

