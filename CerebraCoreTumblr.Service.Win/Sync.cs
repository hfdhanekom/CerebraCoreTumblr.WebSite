using CerebraCoreTumblr.DataAccess.DTO;
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

namespace CerebraCoreTumblr.Service.Win
{
    partial class Sync : ServiceBase
    {
        public Sync()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
        bool Exit = false;
        TumblrBlog SyncingBlog;
        TumblrAccount CurrentAccount;
        string drive;
        string AccountFolder;


        void Runner()
        {
            bool Noted = false;
            bool DoSync = false;
            ITumblrAccount _AccountController = new CerebraCoreTumblr.DataAccess.Controller.TumblrAccount();
            ITumblrBlog _BlogController = new CerebraCoreTumblr.DataAccess.Controller.TumblrBlog();
            while (!Exit)
            {
                try
                {
                    CurrentAccount = _AccountController.GetTumblrAccount("");
                    //CurrentAccount.systemDrive = drive;
                    //CurrentAccount.AccountSystemPath = drive + "\\" + CurrentAccount.username;
                    //AccountFolder = CurrentAccount.AccountSystemPath;
                    SyncingBlog = _BlogController.GetTumblrBlogs(CurrentAccount.id).FirstOrDefault();// Global.mod.tBlogs.Where(ep => ep.DoSync == "true").FirstOrDefault();
                    if (DoSync)
                    {
                        if (SyncingBlog != null)
                        {
                            Process(SyncingBlog);
                        }
                        else
                        {
                            if (!Noted)
                            {
                                Console.WriteLine("No Blog to Sync");
                                Noted = true;
                            }
                        }
                    }

                    //if (Console.KeyAvailable)
                    //{
                    //    String x = Console.ReadLine();
                    //    string[] xarray = x.Split(',');
                    //    if (xarray[0].ToLower() == "add")
                    //    {
                    //        tBlog newBlog = Global.newBlog(Global.ToJson(xarray[1], CurrentAccount.ConsumerKey, 1, 1), CurrentAccount);
                    //        if (newBlog.id == -1)
                    //        {
                    //            Global.mod.tBlogs.Add(newBlog);
                    //            Global.mod.SaveChanges();
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine("Blog Already exists.");
                    //        }
                    //    }
                    //    else if (xarray[0].ToLower() == "exit")
                    //    {
                    //        exit = true;
                    //    }
                    //    else if (xarray[0].ToLower() == "start")
                    //    {
                    //        DoSync = true;
                    //        Console.WriteLine("Sync Started");
                    //    }
                    //    else if (xarray[0].ToLower() == "stop")
                    //    {
                    //        DoSync = false;
                    //        Console.WriteLine("Sync Stoped");
                    //    }
                    //    else if (xarray[0].ToLower() == "config")
                    //    {
                    //        frmConfiguration configure = new frmConfiguration();
                    //        configure.ShowDialog();
                    //        Console.WriteLine("Configuration Saved");
                    //    }
                    //    else if (xarray[0].ToLower() == "chkupdate")
                    //    {
                    //        foreach (tBlog tb in Global.mod.tBlogs.Where(ep => ep.DoSync == "false" && ep.Synced > 0))
                    //        {
                    //            jsRootObject upd = Global.ToJson(tb.BlogName, CurrentAccount.ConsumerKey, 1, 1);
                    //            if (tb.Total != upd.response.blog.posts)
                    //            {
                    //                tb.Total = upd.response.blog.posts;
                    //                tb.Offset = tb.Total - tb.Synced;
                    //                tb.DoSync = "true";
                    //                if (tb.Offset >= 10)
                    //                {
                    //                    tb.Limit = 10;
                    //                }
                    //                else
                    //                {
                    //                    tb.Limit = tb.Offset;
                    //                }

                    //                Global.mod.SaveChanges();
                    //            }

                    //        }
                    //        Console.WriteLine("Check Done");
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Unknown Command");
                    //    }
                    //}
                }
                catch //(Exception ex)
                {
                    //if (MessageBox.Show(ex.Message + ": Would you like to Exit?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //    break;

                    //frmConfiguration configure = new frmConfiguration();
                    //configure.ShowDialog();
                }
            }
        }









         void Process(TumblrBlog blog)
        {
            blog.BlogSystemPath = CurrentAccount.AccountSystemPath + "\\" + blog.blogname;
            Console.WriteLine("Current blog syncing:" + blog.blogname);
            //Sync Posts
            if (blog.CanSync())
            {
                blog.UpdateBlog();
                blog.SyncPosts();

                Console.WriteLine("Blog:" + blog.blogname + " - Current Offset:" + blog.Offset.ToString());
                if (blog.Total == blog.Synced)
                {
                    Console.WriteLine("Blog:" + blog.blogname + " Sync Done.");
                }
            }
        }
    }
}
