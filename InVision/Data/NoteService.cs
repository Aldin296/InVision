namespace InVision.Data
{
    public class NoteService
    {
        public Note selectedNote { get; set; }

        public List<Note> notes = new List<Note>()
        {
            new Note() {Title="Sehr cooler Titel", Content="<h1>Das ist die Note sehr cooler Titel</h1>" },
            new Note() {Title="Untoll", Content="<h1>Frosch</h1>" }
        };

        public List<Note> GettAllNotes()
        {
            return notes;
        }
    }
}
