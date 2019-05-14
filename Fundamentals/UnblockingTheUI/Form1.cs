using System;
using System.Net.Http;
using System.Windows.Forms;

namespace UnblockingTheUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
                LoadWebPageAsync();
        }

        private async void LoadWebPageAsync()
        {
            var url = textBox1.Text;
            if (!url.StartsWith("http://"))
                url = "http://" + url;
            label2.Text = @"Loading...";
            using (var httpClient = new HttpClient())
            {
                var html = await httpClient.GetStringAsync(url);
                label2.Text = html.Length.ToString();
                webBrowser1.DocumentText = html;
            }
        }
    }
}