using InVision_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace InVision_API.Services
{
	public class CalendarService
	{
		private readonly IMongoCollection<User> _userCollection;

		public CalendarService(IOptions<InVisionDatabaseSettings> InVisionDatabaseSettings)
		{
			var mongoClient = new MongoClient(InVisionDatabaseSettings.Value.ConnectionString);
			var mongoDatabase = mongoClient.GetDatabase(InVisionDatabaseSettings.Value.DatabaseName);

			_userCollection = mongoDatabase.GetCollection<User>(InVisionDatabaseSettings.Value.CollectionName);
		}

		public async Task<List<Appointment>> GetAllAppointmentsAsync(string userId)
		{
			var user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();

			return user?.Appointments ?? new List<Appointment>();
		}


		public async Task CreateAppointmentAsync(string userId, Appointment newAppointment)
		{
			var filter = Builders<User>.Filter.Eq(x => x.Id, userId);
			var update = Builders<User>.Update.Push(x => x.Appointments, newAppointment);

			await _userCollection.UpdateOneAsync(filter, update);
		}

		public async Task<Appointment?> GetAppointmentAsync(string userId, string appointmentId)
		{
			var user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();

			return user?.Appointments?.Find(appointment => appointment.Id == appointmentId);
		}
		public async Task DeleteAppointmentAsync(string userId, string appointmentId)
		{
			var filter = Builders<User>.Filter.Eq(x => x.Id, userId);
			var update = Builders<User>.Update.PullFilter("Appointments", Builders<Appointment>.Filter.Eq("_id", appointmentId));

			await _userCollection.UpdateOneAsync(filter, update);
		}

		public async Task UpdateAppointmentAsync(string userId, string appointmentId, Appointment updatedAppointment)
		{
			var filter = Builders<User>.Filter.And(
				Builders<User>.Filter.Eq(x => x.Id, userId),
				Builders<User>.Filter.Eq("Appointments._id", appointmentId)
			);

			var update = Builders<User>.Update
				.Set("Appointments.$.Start", updatedAppointment.Start)
				.Set("Appointments.$.End", updatedAppointment.End)
				.Set("Appointments.$.Text", updatedAppointment.Text)
				.Set("Appointments.$.Color", updatedAppointment.Color);


			await _userCollection.UpdateOneAsync(filter, update);
		}


	}
}
