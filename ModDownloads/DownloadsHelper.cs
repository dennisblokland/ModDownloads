using Microsoft.AspNetCore.Mvc;
using ModDownloads.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModDownloads.Server
{
    public static class DownloadsHelper
    {
        public static Dictionary<DateTime, int> GetDownloadsIncrease(List<Download> downloads)
        {
            Dictionary<DateTime, int> dict = new Dictionary<DateTime, int>();

            for (int key = 0; key < downloads.Count; ++key)
            {
                if (key != 0)
                {
                    int downloadCount = downloads[key].Downloads - downloads[key - 1].Downloads;
                    TimeSpan time = downloads[key].Timestamp - downloads[key - 1].Timestamp;
                    dict.Add(downloads[key].Timestamp, downloadCount);
                }
            }
            return dict;
        }
        public static Dictionary<DateTime, int> GetDownloadsIncrease(Dictionary<DateTime, int> downloads)
        {
            Dictionary<DateTime, int> dict = new Dictionary<DateTime, int>();
            KeyValuePair<DateTime, int> prev = default(KeyValuePair<DateTime, int>);
            foreach (var download in downloads)
            {
                if(prev.Value != 0)
                {
                    int downloadCount = download.Value - prev.Value;
                    dict.Add(download.Key, downloadCount);
                }
                prev = download;
            }
        
            return dict;
        }

    }
}
