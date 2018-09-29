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
using System.Net;
using System.Web;
using System.Collections;

namespace Windows_Image_Scraper
{
    public partial class Form1 : Form
    {

        public static string downloadLocation;




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            downloadLocation = "C:\\temp\\";


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
                string webaddress = txtAddress.Text;
                WebClient wc = new WebClient();

                List<string> imgList = new List<string>();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(wc.OpenRead(webaddress));  //or whatever HTML file you have 

                HtmlNodeCollection imgs = doc.DocumentNode.SelectNodes("//img[@src]");
                if (imgs == null)
                {


                }
                else
                {
                    int i = 0;

                    foreach (HtmlNode imgg in imgs)
                    {

                        try
                        {
                            PictureBox picbox = new PictureBox();
                            picbox.Width = 100;
                            picbox.Height = 100;


                            HtmlAttribute src = imgs[i].Attributes["src"];


                            Panel mypanel = GetImagePanel(src.Value, i);



                            flowLayoutPanel1.Controls.Add(mypanel);

                           

                        }
                        catch
                        {
                            continue;
                        }

                        i++;
                    }


                }



                //open image tab\


                this.tabControl1.SelectedIndex = 1;


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Image GetImage(string url)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(url);

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();

            Bitmap bmp = new Bitmap(responseStream);
            responseStream.Dispose();
            return bmp;
        }


        private Panel GetImagePanel(string url, int rowCount)
        {

            Panel mypanel = new Panel();
            System.Net.WebRequest request = System.Net.WebRequest.Create(url);

           // System.Net.WebResponse response = request.GetResponse();
           // System.IO.Stream responseStream = response.GetResponseStream();

            //Image img = Image.FromStream(responseStream);
           
           // responseStream.Dispose();

            PictureBox picbox = new PictureBox();
            picbox.Name = "PictureBox" + rowCount.ToString();
            //   picbox.Image = img;

            picbox.ImageLocation = url;

            picbox.Width = 75;
            picbox.Height = 75;
            picbox.SizeMode = PictureBoxSizeMode.Zoom;
            mypanel.Controls.Add(picbox);

           
            mypanel.Width = 100;
            mypanel.Height = 100;
            mypanel.BorderStyle = BorderStyle.None;
            mypanel.Name = "panel" + rowCount.ToString();

            CheckBox chk = new CheckBox();
            chk.Top = 80;
            chk.Left = 50;
            chk.Name = rowCount.ToString();
            mypanel.Controls.Add(chk);


           

            return mypanel;


        }

        

        private void btnGo_Click(object sender, EventArgs e)
        {
            if(txtAddress.Text != null && txtAddress.Text != "")
            {
                string newurl = txtAddress.Text;
                webBrowser1.Navigate(newurl);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

            ArrayList picList = new ArrayList();


            foreach(Control c in flowLayoutPanel1.Controls)
            {

                
                if(c is Panel)
                {


                    Panel p = (Panel)c;

                    foreach(Control cc in p.Controls)
                    {

                        

                        if(cc is CheckBox)
                        {

                            CheckBox chk = (CheckBox)cc;
                            
                            if(chk.Checked == true)
                            {

                                Control ctn = p.Controls["PictureBox" + chk.Name];

                                PictureBox picbox = (PictureBox)ctn;

                                picbox.Image.Save(downloadLocation + "\\" + ctn.Name.ToString() + ".png");



                            }

                        }
                    }


                }




            }
        }
    }
}
