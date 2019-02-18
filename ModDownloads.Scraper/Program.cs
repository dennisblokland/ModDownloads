using HtmlAgilityPack;
using ModDownloads.Scraper.Context;
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

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();

        }

        async static Task MainAsync(string[] args)
        {
            using (var db = new DownloadsContext())
            {
                foreach (Mod mod in db.Mod)
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
                    db.Download.Add(download);
                   

                }

                await db.SaveChangesAsync();

            }
        }

    }
}
