using Microsoft.Diagnostics.Instrumentation.Extensions.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UseAsyncSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Random rnd = new Random();
        private AutoResetEvent ev = new AutoResetEvent(true);
        private async void btnStart_Click(object sender, EventArgs e)
        {
            string[] urls = { "https://www.ynet.co.il", "https://www.walla.co.il", "https://www.facebbok.com" };
            int sum = 0;
            for (int i = 0; i < urls.Length; i++)
            {
                try
                {
                    int count = await DownloadFileAsync(urls[i], new Progress<int>( value=>progressBar1.Value = value));
                        sum += count;
                        this.Invoke(new Action(() =>
                        {
                            txtResult.AppendText(urls[i] + " Size : " + count + Environment.NewLine);
                            lblSum.Text = sum.ToString();
                        }));
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        txtResult.AppendText(urls[i] + " failed" + Environment.NewLine);
                    }));
                }
            }
        }
        //private void btnStart_Click(object sender, EventArgs e)
        //{
        //    string[] urls = { "https://www.ynet.co.il", "https://www.walla.co.il", "https://www.facebbok.com" };
        //    int index = 0;
        //    int totalsignal = 0;
        //    int sum = 0;
        //while(totalsignal<=3)
        //    {
        //        try
        //        {
        //            ev.WaitOne();
        //            DownloadFileAsync(urls[index]).ContinueWith(prevTask=>
        //            {
        //                int count = prevTask.Result;
        //                 sum += count;
        //                this.Invoke(new Action(() => 
        //                {
        //                    txtResult.AppendText(urls[index] + " Size : " + count + Environment.NewLine);
        //                    lblSum.Text = sum.ToString();
        //                }));
        //                totalsignal++;
        //                ev.Set();
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            this.Invoke(new Action(() =>
        //            {
        //                txtResult.AppendText(urls[index] + " failed" + Environment.NewLine);
        //            }));
        //        }
        //        index++;
        //    }
        //}
        //private void OnFileDownloadFinished(int result)
        //{
        //    int count = result;
        //    sum += count;
        //    txtResult.AppendText(urls[i] + " Size : " + count + Environment.NewLine);
        //    lblSum.Text = sum.ToString();
        //}

        private Task<int> DownloadFileAsync(string url, IProgress<int> callback)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(50);
                    callback.Report(i);
                }
                return DownloadFile(url);
            });
        }
        private int DownloadFile(string url)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(50);
            }
            return rnd.Next(10, 50);
        }
    }
}
