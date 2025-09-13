using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XoSoRssApp.Helpers;

namespace XoSoRssApp.Controls
{
    public partial class ProvinceRssControl : UserControl
    {
        public ProvinceRssControl()
        {
            InitializeComponent();
        }
        public async Task LoadRssAsync(string rssUrl)
        {
            richTextBox1.Clear();
            var reader = new RssReader();
            var items = await reader.GetRssItemsAsync(rssUrl);

            foreach (var item in items)
            {
                richTextBox1.AppendText($"📢 {item.Title}\n");
                richTextBox1.AppendText($"🕒 {item.PublishDate:g}\n");
                richTextBox1.AppendText($"{item.Description}\n");
                richTextBox1.AppendText(new string('-', 50) + "\n\n");
            }
        }
    }
}
