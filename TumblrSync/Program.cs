using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TumblrSync.Objects.Jason;
using System.Runtime.Serialization.Json;
using System.Data.Entity.Validation;
using System.Configuration;
using System.Windows.Forms;

namespace TumblrSync
{
    class Program
    {
        static tBlog SyncingBlog;
        static tAccount CurrentAccount;
        static string drive;
        static string AccountFolder;
        static void Main(string[] args)
        {

            Console.WriteLine(" ");
            Console.WriteLine("Welcome to CerebraCore Tumblr Sync");
            Console.WriteLine("**********************************");
            Console.WriteLine("************* 2016 ***************");
            Console.WriteLine("**********************************");
            Console.WriteLine("Commands Avalible");
            Console.WriteLine(" ");
            Console.WriteLine(" Start     - Starts the sync.");
            Console.WriteLine(" Stop      - Stops the sync.");
            Console.WriteLine(" Exit      - Exits the application.");
            Console.WriteLine(" Config    - Opens the configuration screen.");
            Console.WriteLine(" Add       - [add,[blogname]] adds a new blog for syncing.");
            Console.WriteLine(" chkupdate - Checks if any of the blogs have updates and if so it setts the do sync to true.");
            Console.WriteLine("");
            Console.WriteLine("***********************************");
            Console.WriteLine("What would you like to do?");


            drive = ConfigurationManager.AppSettings["SysDir"];
            //GetFirstBlogToSync Form DataBase


            bool Noted = false;

            //Update Blog
            bool exit = false;
            bool DoSync = false;
            while (!exit)
            {
                try
                {
                    CurrentAccount = Global.mod.tAccounts.Where(ep => ep.id == 1).FirstOrDefault();
                    CurrentAccount.systemDrive = drive;
                    CurrentAccount.AccountSystemPath = drive + "\\" + CurrentAccount.username;
                    AccountFolder = CurrentAccount.AccountSystemPath;
                    SyncingBlog = Global.mod.tBlogs.Where(ep => ep.DoSync == "true").FirstOrDefault();
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

                    if (Console.KeyAvailable)
                    {
                        String x = Console.ReadLine();
                        string[] xarray = x.Split(',');
                        if (xarray[0].ToLower() == "add")
                        {
                            tBlog newBlog = Global.newBlog(Global.ToJson(xarray[1], CurrentAccount.ConsumerKey, 1, 1), CurrentAccount);
                            if (newBlog.id == -1)
                            {
                                Global.mod.tBlogs.Add(newBlog);
                                Global.mod.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Blog Already exists.");
                            }
                        }
                        else if (xarray[0].ToLower() == "exit")
                        {
                            exit = true;
                        }
                        else if (xarray[0].ToLower() == "start")
                        {
                            DoSync = true;
                            Console.WriteLine("Sync Started");
                        }
                        else if (xarray[0].ToLower() == "stop")
                        {
                            DoSync = false;
                            Console.WriteLine("Sync Stoped");
                        }
                        else if (xarray[0].ToLower() == "config")
                        {
                            frmConfiguration configure = new frmConfiguration();
                            configure.ShowDialog();
                            Console.WriteLine("Configuration Saved");
                        }
                        else if (xarray[0].ToLower() == "chkupdate")
                        {
                            foreach (tBlog tb in Global.mod.tBlogs.Where(ep => ep.DoSync == "false" && ep.Synced > 0))
                            {
                                jsRootObject upd = Global.ToJson(tb.BlogName, CurrentAccount.ConsumerKey, 1, 1);
                                if (tb.Total != upd.response.blog.posts)
                                {
                                    tb.Total = upd.response.blog.posts;
                                    tb.Offset = tb.Total - tb.Synced;
                                    tb.DoSync = "true";
                                    if (tb.Offset >= 10)
                                    {
                                        tb.Limit = 10;
                                    }
                                    else
                                    {
                                        tb.Limit = tb.Offset;
                                    }

                                    Global.mod.SaveChanges();
                                }

                            }
                            Console.WriteLine("Check Done");
                        }
                        else
                        {
                            Console.WriteLine("Unknown Command");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(ex.Message + ": Would you like to Exit?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        break;

                    frmConfiguration configure = new frmConfiguration();
                    configure.ShowDialog();
                }
            }

        }

        static void Process(tBlog blog)
        {
            blog.BlogSystemPath = CurrentAccount.AccountSystemPath + "\\" + blog.BlogName;
            Console.WriteLine("Current blog syncing:" + blog.BlogName);
            //Sync Posts
            if (blog.CanSync())
            {
                blog.UpdateBlog();
                blog.SyncPosts();

                Console.WriteLine("Blog:" + blog.BlogName + " - Current Offset:" + blog.Offset.ToString());
                if (blog.Total == blog.Synced)
                {
                    Console.WriteLine("Blog:" + blog.BlogName + " Sync Done.");
                }
            }
        }
    }

    static class Global
    {
        public static TumblrModel mod = new TumblrModel();

        public static jsRootObject ToJson(string BlogName, string ConsumerKey, int Limit, int Offset)
        {
            String RequestURL = "http://api.tumblr.com/v2/blog/" + BlogName + ".tumblr.com/posts?api_key=" + ConsumerKey + "&offset=[offset]&limit=[limit]";
            var Url1 = RequestURL;
            String ProString = Global.DoRequest(Url1.Replace("[offset]", Offset.ToString()).Replace("[limit]", Limit.ToString()));


            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(jsRootObject));
            jsRootObject obj = null;
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(ProString)))
            {
                obj = (jsRootObject)ser.ReadObject(stream);
            }
            return obj;
        }

        public static String DoRequest(String URL)
        {
            HttpWebRequest Request;
            Request = (HttpWebRequest)WebRequest.Create(URL);
            Request.Method = "GET";
            Request.Timeout = 15000;
            HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
            String result = "";
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            String ProString = "";
            foreach (String xs in result.Split(','))
            {
                if (xs.Contains("\"player\""))
                {
                    if (!xs.Contains("\"width\""))
                    {
                        ProString += xs.Replace("\"player\"", "\"xplayer\"") + ",";
                        continue;
                    }
                }

                ProString += xs + ",";
            }
            ProString = ProString.Substring(0, ProString.Length - 1);
            return ProString;
        }



        public static TumblrTag getTag(String name)
        {
            return mod.TumblrTags.Where(ep => ep.name == name).FirstOrDefault() ?? new TumblrTag { id = -1, name = name };
        }

        public static tBlog newBlog(jsRootObject jsobj, tAccount currentaccount)
        {
            tBlog newB = mod.tBlogs.Where(ep => ep.BlogName == jsobj.response.blog.name).FirstOrDefault() ?? new tBlog { id = -1 };
            if (newB.id > -1)
            {
                newB.BlogName = jsobj.response.blog.name;
                newB.DoSync = "true";
                newB.Limit = 10;
                newB.Offset = jsobj.response.blog.posts;
                newB.RequestURL = "";
                newB.Synced = 0;
                newB.tAccountID = currentaccount.id;
                newB.Total = jsobj.response.blog.posts;
                newB.BlogSystemPath = currentaccount.AccountSystemPath + "\\" + newB.BlogName;
                newB.BlogWebPath = "";
            }
            return newB;
        }
    }

}
