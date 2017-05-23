using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenosBox
{
    public delegate void OnDownloadFinished();
    public delegate void OnDownloadCancelled();


    public class Networking
    {
        private static int margin = 2;
        private static int padding = 12;

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);
        
        public bool IsNetworkConnected()
        {
            Int32 dwFlag = new Int32();
            InternetGetConnectedState(ref dwFlag, 0);
            MessageBox.Show(dwFlag + "");
            return true;
        }

        public void DownloadFileWithForm(string url, string path, OnDownloadFinished onDownloadFinished, OnDownloadCancelled onDownloadCancelled, string title = "Downloading")
        {
            #region Layout

            ProgressBar pbar = new ProgressBar() { Location = new Point(padding, padding) };
            Label msg = new Label() { Location = new Point(padding, margin + padding + 28 + margin) };
            Stopwatch watch = new Stopwatch();
            WebClient client = new WebClient();
            Button finish = new Button() { Text = "Finish" }, cancel = new Button() { Text = "Cancel" };
            Form form = new Form()
            {
                ClientSize = new Size(400, 200),
                MinimumSize = new Size(margin + padding + cancel.Width + 2 * padding + finish.Width + padding + margin,
                                       margin + padding + pbar.Height + 2 * padding + msg.Height + 2 * padding + finish.Height + padding + margin)
            };

            form.Text = title;

            form.Controls.Add(pbar);
            form.Controls.Add(msg);
            form.Controls.Add(finish);
            form.Controls.Add(cancel);

            finish.Enabled = false;

            form.Layout += new LayoutEventHandler((object sender, LayoutEventArgs e) =>
            {
                int formWidth = form.ClientSize.Width;
                int formHeight = form.ClientSize.Height;

                form.SuspendLayout();

                pbar.Width = formWidth - 2 * padding;
                msg.Width = formWidth - 2 * padding;
                finish.Location = new Point(formWidth - padding - finish.Width, formHeight - padding - finish.Height);
                cancel.Location = new Point(formWidth - padding - finish.Width
                                                      - padding - cancel.Width, formHeight - padding - cancel.Height);

                cancel.Click += (object sender_, EventArgs e_) =>
                {
                    client.CancelAsync();
                    form.Close();
                };

                finish.Click += (object sender_, EventArgs e_) =>
                {
                    form.Close();
                };

                form.ResumeLayout(false);
                form.PerformLayout();
            });

            form.Show();

            #endregion

            #region Download

            try
            {
                client.DownloadFileCompleted += new AsyncCompletedEventHandler
                    (
                        (object sender, AsyncCompletedEventArgs e) =>
                        {
                            watch.Reset();
                            finish.Enabled = true;
                            if (e.Cancelled)
                            {
                                msg.Text += "\nDownload Cancelled.";
                                onDownloadCancelled();
                            }
                            else
                            {
                                msg.Text += "\nDownload Finished.";
                                onDownloadFinished();
                            }
                        }
                    );
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler
                    (
                        (object sender, DownloadProgressChangedEventArgs e) =>
                        {
                            pbar.Value = e.ProgressPercentage;
                            msg.Text = e.BytesReceived + " / " + e.TotalBytesToReceive + "\n" + (int)(e.BytesReceived / ((watch.ElapsedMilliseconds + 1.0)/ 1000)) + " byte/s";
                        }
                    );

                if (IsNetworkConnected())
                {
                    watch.Start();
                    client.DownloadFileAsync(new Uri(url), path);
                }
                else
                {
                    msg.Text = "Network Error.";
                    finish.Enabled = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            #endregion
        }
    }
}
