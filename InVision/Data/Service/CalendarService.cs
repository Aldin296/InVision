using InVision.Data.Model;
using System.Text.Json;

namespace InVision.Data.Service
{
    public class CalendarService
    {
		private readonly string baseurl = "http://localhost:80";
		HttpClient client = new HttpClient();
		public Appointment selectedAppointment{ get; set; }
		List<Appointment> appointments= new List<Appointment>() { };


		public async Task<List<Appointment>> GetAllAppointmentsAsync(string userid)
		{
			string requestUrl = $"{baseurl}/api/User";
			var data = await client.GetAsync($"{requestUrl}/{userid}");
			User user = null;
			if (data.StatusCode != System.Net.HttpStatusCode.NoContent)
			{
				string content = await data.Content.ReadAsStringAsync();
				user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
			}

			return user?.Appointments ?? new List<Appointment>();
		}

        public async Task CreateAppointmentAsync(string userid, Appointment newAppointment)
        {
            string requestUrl = $"{baseurl}/api/Calendar/{userid}";
            await client.PostAsJsonAsync(requestUrl, newAppointment);
        }

		public async Task UpdateAppointmentAsync(string userid, string appointmentId, Appointment updatedAppointment)
		{
			string requestUrl = $"{baseurl}/api/Calendar/{userid}/{appointmentId}";
			await client.PutAsJsonAsync(requestUrl, updatedAppointment);
		}
		public async Task DeleteAppointmentAsync(string userid, string appointmentId)
		{
			string requestUrl = $"{baseurl}/api/Calendar/{userid}/{appointmentId}";
			await client.DeleteAsync(requestUrl);
		}
	}
}
