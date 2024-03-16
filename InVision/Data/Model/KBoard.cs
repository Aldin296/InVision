namespace InVision.Data.Model
{
    public class KBoard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public List<TodoItem>? Items { get; set; } = new List<TodoItem>();

        public KBoard()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
