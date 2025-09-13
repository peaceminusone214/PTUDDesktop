using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XoSoRssApp.Controls;

namespace XoSoRssApp
{
    public partial class FormMain : Form
    {
        private ProvinceRssControl provinceControl;

        private Dictionary<string, Dictionary<string, string>> rssFeeds = new Dictionary<string, Dictionary<string, string>>
{
    {
        "Miền Bắc", new Dictionary<string, string>
        {
            { "Xổ số miền Bắc", "https://kqxs.net.vn/rss-feed/xo-so-mien-bac-xsmb-xstd.rss" },
            { "Xổ số điện toán 123", "https://kqxs.net.vn/rss-feed/xo-so-dien-toan-123-xsdt123.rss" }
        }
    },
    {
        "Miền Trung", new Dictionary<string, string>
        {
            { "Đà Lạt", "https://kqxs.net.vn/rss-feed/xo-so-da-lat-xsdl-xsld.rss" },
            { "Miền Trung tổng hợp", "https://kqxs.net.vn/rss-feed/mien-trung-xsmt.rss" }
        }
    },
    {
        "Miền Nam", new Dictionary<string, string>
        {
            { "An Giang", "https://kqxs.net.vn/rss-feed/xo-so-an-giang-xsag.rss" },
            { "Bình Dương", "https://kqxs.net.vn/rss-feed/xo-so-binh-duong-xsbd.rss" },
            { "Cà Mau", "https://kqxs.net.vn/rss-feed/xo-so-ca-mau-xscm.rss" },
            { "TP.HCM", "https://kqxs.net.vn/rss-feed/xo-so-tphcm-xshcm.rss" },
            { "Sóc Trăng", "https://kqxs.net.vn/rss-feed/xo-so-soc-trang-xsst.rss" },
            { "Miền Nam tổng hợp", "https://kqxs.net.vn/rss-feed/mien-nam-xsmn.rss" }
        }
    }
};


        public FormMain()
        {
            InitializeComponent();
            LoadTreeView();
            SetupUserControl();
        }

        private void LoadTreeView()
        {
            treeView1.Nodes.Clear();

            foreach (var region in rssFeeds)
            {
                var parentNode = new TreeNode(region.Key);
                foreach (var province in region.Value)
                {
                    var childNode = new TreeNode(province.Key) { Tag = province.Value };
                    parentNode.Nodes.Add(childNode);
                }
                treeView1.Nodes.Add(parentNode);
            }

            treeView1.AfterSelect += TreeView1_AfterSelect;
        }

        private void SetupUserControl()
        {
            provinceControl = new ProvinceRssControl
            {
                Dock = DockStyle.Fill
            };
            panelContent.Controls.Add(provinceControl);
        }

        private async void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null) return;

            string rssUrl = e.Node.Tag.ToString();
            await provinceControl.LoadRssAsync(rssUrl);
        }

    }
}
