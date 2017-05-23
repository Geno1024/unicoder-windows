using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

using Unicoder.Properties;

namespace Unicoder
{
    class UCD
    {
        Label msg = new Label() { Location = new Point(12, 12 + 28 + 2 + 2) };
        ProgressBar pbar = new ProgressBar() { Location = new Point(12, 12) };
        Form form = new Form() { ClientSize = new Size(400, 200) };
        Stopwatch watch = new Stopwatch();
        WebClient client = new WebClient();
        Button finish = new Button(), cancel = new Button();

        public UCD()
        {
            //MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            form.Text = Resources.updateUCD;

            form.Controls.Add(pbar);
            form.Controls.Add(msg);

            Draw();

            form.Resize += new EventHandler((object sender, EventArgs e) => { Draw(); });

            form.Show();

            DownloadDatabase();
        }

        private void Draw()
        {
            int formWidth = form.ClientSize.Width;
            int formHeight = form.ClientSize.Height;

            form.SuspendLayout();

            pbar.Width = formWidth - 12 - 12;
            msg.Width = formWidth - 12 - 12;

            form.ResumeLayout(false);
            form.PerformLayout();
        }

        public void DownloadDatabase()
        {
            try
            {
                string url = "http://www.unicode.org/Public/UCD/latest/ucd/UCD.zip";
                client.DownloadFileCompleted += new AsyncCompletedEventHandler
                    (
                        (object sender, AsyncCompletedEventArgs e) =>
                        {
                            watch.Reset();
                            if (e.Cancelled)
                            {
                                msg.Text = Resources.downloadCancel;
                            }
                            else
                            {
                                msg.Text += "\n" + Resources.downloadOK;
                                UnpackDatabase();
                            }
                        }
                    );
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler
                    (
                        (object sender, DownloadProgressChangedEventArgs e) =>
                        {
                            pbar.Value = e.ProgressPercentage;
                            msg.Text = e.BytesReceived + " / " + e.TotalBytesToReceive;
                        }
                    );
                watch.Start();
                client.DownloadFileAsync(new Uri(url), "./UCD.zip");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void UnpackDatabase()
        {

        }
    }
}
