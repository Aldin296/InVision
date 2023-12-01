namespace InVision.Data
{
    public class KBoardService
    {
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
    }
}
