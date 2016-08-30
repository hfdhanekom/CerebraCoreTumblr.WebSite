using CerebraCoreTumblr.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using CerebraCoreTumblr.DataAccess.DTO;
using System.IO;

namespace CerebraCoreTumblr.Service.Win
{
    partial class Downloader : ServiceBase
    {
        public Downloader()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            Exit = false;
            Runner();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            Exit = true;
        }

        IDownload ToDownload;

        bool Exit = false;
        void Runner()
        {
            foreach (Download xo in ToDownload.CHKDownloads())//.tDownloads.Where(ep => ep.Downloaded == "true" && ep.PostLinkID != null))
            {
                if (!File.Exists(xo.FileFullName))
                {
                    xo.Completed = false;
                }
            }
            //mod.SaveChanges();

            bool Noted = false;
            bool DoDownload = false;
            while (!Exit)
            {
                try
                {
                    if (DoDownload)
                    {
                        
                        ToDownload = new CerebraCoreTumblr.DataAccess.Controller.Download();
                        Console.WriteLine("Checking for downloads");
                        int HasDownloads = ToDownload.GetDownloads().Count(); //mod.tDownloads.Where(ep => ep.Downloaded == "false" && ep.PostLinkID != null).Count();
                        if (!Noted)
                        {
                            Console.WriteLine(HasDownloads.ToString() + " downloads found");
                            Noted = true;
                        }
                        if (HasDownloads > 0)
                        {
                            Console.WriteLine("Downloading 10.....");
                            List<Download> dlist = ToDownload.GetDownloads().Take(10).ToList();
                            Parallel.ForEach(
                                dlist,
                                new ParallelOptions { MaxDegreeOfParallelism = dlist.Count },
                                _Download);

                            foreach (Download dl in dlist)
                            {
                                ITumblrPost iP = new CerebraCoreTumblr.DataAccess.Controller.TumblrPost();
                                TumblrPost ps = iP.GetPost(dl.PostLinkID);
                                if (ps != null)
                                {
                                    ps.Downloaded = dl.Completed;
                                }
                            }

                            //mod.SaveChanges();
                            Noted = false;
                        }
                        else
                        {

                            if (!Noted)
                            {

                                Console.WriteLine("No Download.");
                                Noted = true;
                            }
                            DoDownload = false;
                            Console.WriteLine("Download Stoped.");
                            Noted = false;
                        }
                    }

                }
                catch //(Exception ex)
                {
                    //if (MessageBox.Show(ex.Message + ": Would you like to Exit?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //    break;

                }
            }
        }

        public void _Download(Download download)
        {
            ToDownload._Download(download);
        }

    }
}
