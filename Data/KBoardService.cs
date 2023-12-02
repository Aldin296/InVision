namespace InVision.Data
{
    public class KBoardService
    {
        List<List<TodoItem>> todoItems = new List<List<TodoItem>>()
        {
            new List<TodoItem>()
            {
                new TodoItem(){Title = "Item 1", Description="abc"},
                new TodoItem(){Title = "Item 2", Description="abc"},
                new TodoItem(){Title = "Item 3", Description="abc"},
                new TodoItem(){Title = "Item 4", Description="444442"},
                new TodoItem(){Title = "Item 5", Description="3a4bc"},
                new TodoItem(){Title = "Item 7", Description="ad2bc"},
            },
            new List<TodoItem>()
            {
                new TodoItem(){Title = "Item 8", Description="ad a dw2bc"},
                new TodoItem(){Title = "Item 9", Description="abc"},
                new TodoItem(){Title = "Item 10", Description="abcde"},
            },
            new List<TodoItem>()
            {
                new TodoItem(){Title = "Item 11", Description="adsa fas faw bc"},
                new TodoItem(){Title = "Item 12", Description="a sdf asbc"},
                new TodoItem(){Title = "Item 13", Description="abcde"},
            }
        };

        List<KBoard> boards = new List<KBoard>()
        {
            new KBoard(){Name="Projekt1", Description="hier wird beschrieben das in diesem Board verwaltet wird", CreatedBy="User1"},
            new KBoard(){Name="Weise Haus", Description="Ex Präsidenten Buisness", CreatedBy="Obama"},
            new KBoard(){Name="Weise Haus 2022", Description="Gelbe Präsidenten Buisness", CreatedBy="Trump"},
            new KBoard(){Name="InVision", Description="Diplomarbeit - WebbPlaner für Menschen ohne Plan", CreatedBy="Jan"},
            new KBoard(){Name="Test", Description="bla bla loren impus lolol", CreatedBy="Gott"},
            new KBoard(){Name="spas und gas", Description="LOL", CreatedBy="Rizzler"}
        };

       

        public async Task<List<KBoard>> BoardList()
        {
            return await Task.FromResult(boards);
        }

        public KBoard GetKBoardByName(string name)
        {
            return boards.Where(board => board.Name == name).FirstOrDefault();
        }
    }
}
