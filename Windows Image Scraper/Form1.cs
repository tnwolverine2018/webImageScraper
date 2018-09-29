using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Windows_Image_Scraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string currenturl = webBrowser1.Url.ToString();
            this.txtAddress.Text = currenturl;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {




            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if(txtAddress.Text != null && txtAddress.Text != "")
            {
                string newurl = txtAddress.Text;
                webBrowser1.Navigate(newurl);
            }
        }
    }
}
