﻿using InVision.Data.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using System.Text.Json;
using System.Xml.Linq;

namespace InVision.Data.Service

{
    public class KBoardService
    {
        private readonly string baseurl = "http://localhost:80";
        HttpClient client = new HttpClient();
        public KBoard selectedBoard { get; set; }
        List<KBoard> boards = new List<KBoard>() { };

        /*public async Task<List<KBoard>> BoardList()
        {
            return await Task.FromResult(boards);
        }*/
        /*public async Task<KBoard?> GetKBoardAsync(string userId, string kboardId)
        {
            var user = await CurrentUser.Find(x => x.Id == userId).FirstOrDefaultAsync();


            return user?.KBoards?.Find(kboard => kboard.Id == kboardId);
        }*/

        public KBoard GetKBoardByName(string name)
        {
            return boards.Where(board => board.Name == name).FirstOrDefault();
        }

        public KBoard GetBoardById(string id)
        {
            return boards.Where(board => board.Id == id).FirstOrDefault();
        }


        public async Task<List<KBoard>> GetAllKBoardsAsync(string userid)
        {
            string requestUrl = $"{baseurl}/api/User";
            var data = await client.GetAsync($"{requestUrl}/{userid}");
            User user = null;
            if (data.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                string content = await data.Content.ReadAsStringAsync();
                user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return user?.KBoards ?? new List<KBoard>();
        }


        public async Task CreateKBoard(string userid, KBoard kboard)
        {
            string requestUrl = $"{baseurl}/api/KBoard/{userid}";
            await client.PostAsJsonAsync(requestUrl, kboard);
        }

        public async Task DeleteKBoard(string userid, string kboardid)
        {
            string requestUrl = $"{baseurl}/api/KBoard/{userid}/{kboardid}";
            await client.DeleteAsync(requestUrl);
        }

        public async Task UpdateKBoard(string userid, string kboardid,KBoard newKboard)
        {
			string requestUrl = $"{baseurl}/api/KBoard/{userid}/{kboardid}";
            await client.PutAsJsonAsync(requestUrl,newKboard);

		}

	}
}
