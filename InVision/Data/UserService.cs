
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.Json;

namespace InVision.Data

{
    public class UserService
    {
        private readonly string baseurl = "http://localhost:80";
        HttpClient client = new HttpClient();

        public HashPassword hashPassword(string password)
        {
            
            //Salt wird generiert um bei dem selben Passwort von verschiedenen Benutzern einen anderen Hashwert zu generieren
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            Console.WriteLine($"Hashed: {hashed}");

            //Um sowohl den gehashten wert als auch den salt zu speichern und zu returnen aber trotzdem das hashen in 
            //dieser funktion ausgelagert zu haben wird eine zusätzliche Klasse "Hashpassword" als returnwert benutzt.
            HashPassword hashedPassword = new HashPassword();
            hashedPassword.Hashed = hashed;
            hashedPassword.Salt = salt;
            return hashedPassword;
            
        }

        public async Task<bool> PasswordControl(string? userid, string? password)
        {
            string requestUrl = $"{baseurl}/api/User/{userid}";
            User u = await client.GetFromJsonAsync<User>(requestUrl);
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
            else
            {
                return false;
            }

        }

        public async Task CreateUser(string userName, string userPassword, string userEmail)
        {
            string requestUrl = $"{baseurl}/api/User";

            HashPassword hashPass = hashPassword(userPassword);

            //Console.WriteLine(usernameValue + emailValue + passwordValue);
            User newUser = new User(userName, hashPass.Hashed, userEmail);
            newUser.salt = hashPass.Salt;



            await client.PostAsJsonAsync(requestUrl, newUser);
        }

        public async Task UpdateUser(string id, User newUser)
        {
            string requestUrl = $"{baseurl}/api/User/{id}";
            await client.PutAsJsonAsync(requestUrl, newUser);

        }


        public async Task<bool> LoginUser(string email, string password)
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
                    foreach (User u in users)
                    {
                        if (email == u.Email)
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

        

        public async Task<User> GetUserById(string id)
        {
            string requestUrl = $"{baseurl}/api/User";
            var data = await client.GetAsync($"{requestUrl}/{id}");
            User user = null;
            if (data.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                string content = await data.Content.ReadAsStringAsync();
                user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return user;
        }
        public async Task<User> GetUserByEmail(string email)
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

                    foreach (User u in users)
                    {
                        if (email == u.Email)
                        {
                            return u;
                        }
                    }
                }
            }
            return null;
        }

    }
}
