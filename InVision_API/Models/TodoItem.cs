namespace InVision_API.Models
{
    public class TodoItem
    {
            public string? Id { get; set; }
            public string Title { get; set; }
            public string? Description { get; set; }
            public int State { get; set; }

        public TodoItem()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
