namespace InVision.Data
{
    public class KBoardService
    {
        public KBoard selectedBoard { get;set; }

        List<KBoard> boards = new List<KBoard>()
        {
            new KBoard(){Id=1, Name="Projekt1", Description="hier wird beschrieben das in diesem Board verwaltet wird", CreatedBy="User1", Items = { new TodoItem { Title="title", Description="ich tue dies und das", state=0}, new TodoItem {Title="ich mache etwas", Description="ich mache tatsachlich etwas lol", state=1 }, new TodoItem {Title="haha lol invision", Description="diplomarbeit für die schule ich ahbe fun", state=2} } },
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

        public KBoard GetBoardById(int id)
        {
            return boards.Where(board => board.Id == id).FirstOrDefault();
        }
    }
}
