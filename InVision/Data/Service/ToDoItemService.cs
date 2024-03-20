using InVision.Data.Model;
using System.Text.Json;

namespace InVision.Data.Service
{
    public class ToDoItemService
    {
        private readonly string baseurl = "http://localhost:80";
        HttpClient client = new HttpClient();
        public TodoItem selectedAppointment { get; set; }
        List<TodoItem> todoitems = new List<TodoItem>() { };

        public async Task CreateItem(string userid, string kboardid,TodoItem item)
        {
            string requestUrl = $"{baseurl}/api/ToDoItem/{userid}/{kboardid}";
            await client.PostAsJsonAsync(requestUrl, item);
        }

        public async Task<List<TodoItem>> GetAllItemsAsync(string userid, string kboardid)
        {
            string requestUrl = $"{baseurl}/api/ToDoItem";
            var data = await client.GetAsync($"{requestUrl}/{userid}/{kboardid}");
            KBoard kboard = null;
            if (data.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                string content = await data.Content.ReadAsStringAsync();
                kboard = JsonSerializer.Deserialize<KBoard>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return kboard?.Items ?? new List<TodoItem>();
        }
            public async Task DeleteItem(string userid, string kboardId, string itemid)
            {
                string requestUrl = $"{baseurl}/api/ToDoItem/{userid}/{kboardId}/{itemid}";
                await client.DeleteAsync(requestUrl);
            }


		    public async Task UpdateItem(string userId, string kboardId, string itemid,TodoItem item)
		    {
			    string requestUrl = $"{baseurl}/api/ToDoItem/{userId}/{kboardId}/{itemid}";
			    await client.PutAsJsonAsync(requestUrl, item);
		    }
	}
}
