using InVision.Data.Model;
using System.Text.Json;
namespace InVision.Data.Service

{
    public class NoteService
    {

        private readonly string baseurl = "http://localhost:80";
        HttpClient client = new HttpClient();
        public Note selectedNote { get; set; }

        public List<Note> notes = new List<Note>(){};

        public async Task<List<Note>> GetAllNotesAsync(string userid)
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

            return user?.Notes ?? new List<Note>();
        }
        public Note GetNoteByName(string title)
        {
            return notes.Where(note => note.Title == title).FirstOrDefault();
        }

        public Note GetNoteById(string id)
        {
            return notes.Where(note => note.Id == id).FirstOrDefault();
        }

        public async Task CreateNote(string userid, Note note)
        {
            string requestUrl = $"{baseurl}/api/Note/{userid}";
            await client.PostAsJsonAsync(requestUrl, note);
        }

        public async Task DeleteNote(string userid, string noteid)
        {
            string requestUrl = $"{baseurl}/api/Note/{userid}/{noteid}";
            await client.DeleteAsync(requestUrl);
        }

        public async Task UpdateNote(string userid, string noteid, Note note)
        {
            string requestUrl = $"{baseurl}/api/Note/{userid}/{noteid}";
            await client.PutAsJsonAsync(requestUrl, note);
        }
    }
}
