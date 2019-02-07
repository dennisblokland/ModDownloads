using HtmlAgilityPack;
using ModDownloads.Shared.Entities;
using ModDownloads.Shared.Server.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ModScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
   
        }

        async static Task MainAsync(string[] args)
        {
            using (var context = new DownloadsContext())
            {
                foreach(Mod mod in context.Mod)
                {
                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync(mod.URL);
                    var pageContents = await response.Content.ReadAsStringAsync();

                    HtmlDocument pageDocument = new HtmlDocument();
                    pageDocument.LoadHtml(pageContents);

                    var headlineText = pageDocument.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div[2]/div/section/div[2]/div[1]/div[2]/ul/li[4]/div[2]").InnerText;
                    int downloads = Int32.Parse(headlineText.Replace(",", ""));
                    var download = new Download()
                    {
                        Downloads = downloads,
                        Mod = mod
                    };
                    context.Download.Add(download);
                   
                }

                context.SaveChanges();
            }
 
        }


    }

}
