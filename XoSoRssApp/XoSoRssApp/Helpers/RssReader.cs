using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Linq;

namespace XoSoRssApp.Helpers
{
    public class RssReader
    {
        public async Task<List<RssItem>> GetRssItemsAsync(string url)
        {
            var list = new List<RssItem>();
            try
            {
                using (var client = new HttpClient())
                {
                    var xml = await client.GetStringAsync(url);
                    var doc = XDocument.Parse(xml);

                    foreach (var item in doc.Descendants("item"))
                    {
                        list.Add(new RssItem
                        {
                            Title = item.Element("title")?.Value ?? "",
                            Description = item.Element("description")?.Value ?? "",
                            Link = item.Element("link")?.Value ?? "",
                            PublishDate = DateTime.TryParse(item.Element("pubDate")?.Value, out var dt) ? dt : DateTime.MinValue
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                list.Add(new RssItem
                {
                    Title = "Lỗi",
                    Description = ex.Message
                });
            }
            return list;
        }
    }
}
