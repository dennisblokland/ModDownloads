using HtmlAgilityPack;
using ModDownloads.Shared.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ModDownloads.Scraper
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string baseURL = "http://localhost:5000/api";

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();

        }

        async static Task MainAsync(string[] args)
        {
            var josnresponse = client.GetAsync(baseURL + "/mods/").Result;
            var jsonString = josnresponse.Content.ReadAsStringAsync();
            jsonString.Wait();
            List<Mod> mods = JsonConvert.DeserializeObject<List<Mod>>(jsonString.Result);


            foreach (Mod mod in mods)
            {
                var response = await client.GetAsync(mod.URL);
                var pageContents = await response.Content.ReadAsStringAsync();

                HtmlDocument pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContents);

                var headlineText = pageDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div[2]/div/section/div[2]/div[1]/div[2]/ul/li[4]/div[2]").InnerText;
                int downloads = Int32.Parse(headlineText.Replace(",", ""));
                var download = new Download()
                {
                    Downloads = downloads,
                    ModId = mod.Id
                };
                var json = JsonConvert.SerializeObject(download);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                await client.PostAsync(baseURL + "/Downloads/", stringContent);
            }


        }

    }
}
