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
        static string drive = ConfigurationManager.AppSettings["SysDir"];
        static string AccountFolder;

        static bool Noted = false;
        static bool exit = false;
        static bool DoSync = false;

        static void Main(string[] args)
        {

            Console.WriteLine(" ");
            Console.WriteLine("Welcome to CerebraCore Tumblr Sync");
            Console.WriteLine("**********************************");
            Console.WriteLine("************* 2016 ***************");
            Console.WriteLine("**********************************");
            
            if (InitialiseDB())
            {
                Noted = false;
                exit = false;
                DoSync = false;
            }
            else
            {
                exit = true;
                DoSync = false;
            }

            while (!exit)
            {
                if (!DoSync)
                    ShowMenue();
                GO();
            }

            Console.WriteLine("Goodbye.....");
        }

        static bool InitialiseDB()
        {
            do
            {
                Console.WriteLine("Checking Database Connection...Please wait...");
                if (Global.DBConnected)
                {
                    Console.WriteLine("Connected to Database.");
                    return true;
                }
                else
                {
                    if (MessageBox.Show("Connecting to Database Failed: Would you like to Exit?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        return false;
                    }
                    else
                    {
                        frmConfiguration configure = new frmConfiguration();
                        configure.ShowDialog();
                    }
                }
            } while (true);
        }

        static void GO()
        {
            try
            {
                CurrentAccount = Global.mod.tAccounts.Where(ep => ep.id == 2).FirstOrDefault();
                if (CurrentAccount != null)
                {
                    CurrentAccount.systemDrive = drive;
                    CurrentAccount.AccountSystemPath = drive + "\\" + CurrentAccount.username;
                    AccountFolder = CurrentAccount.AccountSystemPath;
                    SyncingBlog = Global.mod.tBlogs.Where(ep => ep.DoSync == "true").FirstOrDefault();

                    if (!Noted && SyncingBlog == null)
                    {
                        ShowMenue();
                        Noted = true;
                        //add,blog,gayillustrations
                    }

                    if (DoSync)
                    {
                        if (SyncingBlog != null)
                        {
                            Process(SyncingBlog);
                        }
                        else
                        {
                            Noted = false;
                            if (!Noted)
                            {
                                Console.WriteLine("No Blog to Sync");

                            }
                        }
                    }

                    if (Console.KeyAvailable)
                    {
                        String x = Console.ReadLine();
                        processCMD(x);
                    }
                }
                else
                {
                    if (!Noted)
                    {
                        Console.WriteLine("No Tumblr Accounts Found.");
                        ShowMenue();
                        Noted = false;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            //catch (DBUpdateException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //}
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message + ": Would you like to Exit?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    exit = true;
                    return;
                }

                frmConfiguration configure = new frmConfiguration();
                configure.ShowDialog();
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

        static void ShowMenue()
        {
            Console.WriteLine("");
            Console.WriteLine("Commands Avalible");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(" Start     - Starts the sync.");
            Console.WriteLine(" Stop      - Stops the sync.");
            Console.WriteLine(" Exit      - Exits the application.");
            Console.WriteLine(" Config    - Opens the configuration screen.");
            Console.WriteLine(" Add       - [add,blog,[blogname]] adds a new blog for syncing.");
            Console.WriteLine(" Add       - [add,acc] adds a new blog for syncing.");
            Console.WriteLine(" List      - list,blog lists all blogs for account.");
            Console.WriteLine(" Reset     - reset,blog,[blogname]");
            Console.WriteLine(" CMD       - Displays this.");
            Console.WriteLine(" chkupdate - Checks if any of the blogs have updates and if so it setts the do sync to true.");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("What would you like to do?");
            string x = Console.ReadLine();
            processCMD(x);
        }

        static void processCMD(String input)
        {
            string[] xarray = input.Split(',');
            if (xarray[0].ToLower() == "cmd")
            {
                ShowMenue();
            }
            else if (xarray[0].ToLower() == "add")
            {
                if (xarray[1].ToLower() == "blog" && CurrentAccount != null)
                {
                    tBlog newBlog = Global.newBlog(Global.ToJson(xarray[2], CurrentAccount.ConsumerKey, 1, 1), CurrentAccount);
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
                else if (xarray[1].ToLower() == "acc")
                {
                    frmAccount acc = new frmAccount();

                    if (acc.ShowDialog() == DialogResult.OK)
                    {
                        Global.mod.tAccounts.Add(acc.newAcc);
                        Global.mod.SaveChanges();
                    }
                }
            }
            else if (xarray[0].ToLower() == "list")
            {
                if (xarray[1].ToLower() == "blog")
                {
                    if (CurrentAccount != null)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Blogs for:{0}",CurrentAccount.username);
                        Console.WriteLine("-------------------------------------");
                        int Counterxx = 0;
                        foreach (tBlog tb in CurrentAccount.tBlogs)
                        {
                            Console.WriteLine("{0} Blogname:{1}, DoSync:{2}, Total:{3}, Synced:{4}, CurrentlyAt:{5} ",Counterxx,tb.BlogName,tb.DoSync,tb.Total,tb.Synced,tb.Offset);
                            Counterxx++;
                        }
                        Console.WriteLine("-------------------------------------");
                    }
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
            else if (xarray[0].ToLower() == "reset")
            {
                if (xarray[1].ToLower() == "blog")
                {
                    String Blogx = xarray[2];
                    tBlog resetBlog = Global.mod.tBlogs.Where(ep => ep.BlogName == Blogx).First();
                    if (resetBlog != null)
                    {

                        TumblrBlog remBlog = Global.mod.TumblrBlogs.Where(ep => ep.name == resetBlog.BlogName).First();
                        if (remBlog != null)
                        {
                            Global.mod.TumblrBlogs.Remove(remBlog);
                        }
                        resetBlog.DoSync = "true";
                        resetBlog.Limit = 10;
                        resetBlog.Offset = resetBlog.Total;
                        resetBlog.Synced = 0;
                        foreach (tDownload dl in Global.mod.tDownloads.Where(ep => ep.tBlogID == resetBlog.id))
                        {
                            Global.mod.tDownloads.Remove(dl);
                        }
                        Global.mod.SaveChanges();
                    }
                }
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

    static class Global
    {
       public static TumblrModel mod = new TumblrModel();

       public static Boolean DBConnected
       {
          
           get
           {
               try
               {
                   mod.Database.Connection.Open();
                   mod.Database.Connection.Close();
                   return true;
               }
               catch
               {
                   return false;
               }
           }
       }

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
           if (newB.id == -1)
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
