using MongoDB.Bson.IO;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text.Json;

namespace InVision.Data
{
	public class MatIconService
	{
		public MatRoot? matroot = new();

		public async Task<MatRoot> GetAllMaterialDesignIcons()
		{
			MatRoot root = new();
			using (HttpClient client = new HttpClient())
			{

				var response = await client.GetAsync("https://fonts.google.com/metadata/icons?key=material_symbols&incomplete=true");
                Console.WriteLine(response.Content);
                response.EnsureSuccessStatusCode();
				string jsonString = await response.Content.ReadAsStringAsync();
				root = JsonSerializer.Deserialize<MatRoot>(jsonString);
				matroot = root;

			}
			return matroot;
		}

		public async Task<List<string>> GetMaterialDesignIconNames()
		{
			List<string> iconNameList = new List<string>();
			await GetAllMaterialDesignIcons();
			foreach (MatIcon icon in matroot.Icons)
			{
				iconNameList.Add(icon.Name);
			}

			return iconNameList;
		}
	}
}
