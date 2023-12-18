
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InVision.Data

{
	public class UserService
	{
		private readonly string baseurl = "https://localhost:7133";
		HttpClient client = new HttpClient();

		public async Task CreateUser(User newUser)
		{
			string requestUrl = $"{baseurl}/api/User";
			await client.PostAsJsonAsync(requestUrl, newUser);
		}

        public async Task<bool> LoginUser(string email,string password)
		{
            string requestUrl = $"{baseurl}/api/User";
            var data = await client.GetAsync(requestUrl);
            if (data.IsSuccessStatusCode)
            {
                if (data.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    string content = await data.Content.ReadAsStringAsync();
                    List<User> users = JsonSerializer.Deserialize<List<User>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    foreach(User u in users)
                    {
                        if(email == u.Email)
                        {
                            byte[] salt = u.salt;

                            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
                            string controllhashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: password!,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 100000,
                                numBytesRequested: 256 / 8));
                            if (controllhashed == u.Password)
                            {
                                return true;
                            }

                        }
                       
                    }
                   
                }
            }
           
			
			return false;
        }
    }
}
