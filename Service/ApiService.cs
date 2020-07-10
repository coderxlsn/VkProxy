using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkProxy.Models;

namespace VkProxy.Service
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;
        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetName(string tag)
        {
            var result = await GetApi(tag);
            return result?.Name;
        }

        public async Task<string> CheckClub(ClubModel model)
        {
            if (model.Tag.Length < 2 && !model.Tag.StartsWith("#"))
            {
                return "False4";
            }
            string idsconnect = _configuration.GetConnectionString("ids");
            List<string> ids = idsconnect.Split(",").ToList();
            var result = await GetApi(model.Tag);
            if (result != null)
            {
                int legend = 0;
                foreach (PlayerModel.Brawler row in result.Brawlers)
                {
                    if (ids.Find(x => x == row.Id.ToString()) != null)
                    {
                        legend++;
                    }
                }

                if (result.Club.Name == null)
                {
                    return (model.Legendarn <= legend && model.AllPlayer == result.Brawlers.Count() ? "No" : "False" + model.ClubName+":"+model.Legendarn+":"+model.AllPlayer);
                }

                return (model.Legendarn <= legend && model.ClubName.Trim() == result.Club.Name &&
                        model.AllPlayer == result.Brawlers.Count()
                    ? "True"
                    : "False:" + model.ClubName+":"+model.Legendarn+":"+model.AllPlayer);

            }

            return "False1";
        }
        public async Task<bool> CheckId(ParamModel model)
        {
            if (model.Tag.Length < 2 && !model.Tag.StartsWith("#"))
            {
                return false;
            }
            string idsconnect = _configuration.GetConnectionString("ids");
            List<string> ids = idsconnect.Split(",").ToList();
            var result = await GetApi(model.Tag);
            if (result != null)
            {
                int legend = 0;
                foreach (PlayerModel.Brawler row in result.Brawlers)
                {
                    if (ids.Find(x => x == row.Id.ToString()) != null)
                    {
                        legend++;
                    }
                }

                return model.Legendarn <= legend && model.AllPlayer == result.Brawlers.Count();

            }

            return false;
        }

        private async Task<PlayerModel> GetApi(string tag)
        {
            tag = tag.Replace("#", "%23");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _configuration.GetConnectionString("token"));
            client.BaseAddress = new Uri("https://api.brawlstars.com/");
            var responce = await client.GetAsync("/v1/players/" + tag);
            if (responce.IsSuccessStatusCode)
            {
                string body = await responce.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PlayerModel>(body);

            }

            return null;
        }
    }
}
