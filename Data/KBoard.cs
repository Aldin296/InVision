namespace InVision.Data
{
    public class KBoard
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public List<TodoItem> Items { get; set; } = new List<TodoItem>();
    }
}
