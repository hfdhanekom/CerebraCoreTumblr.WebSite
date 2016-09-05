using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TumblrDownloader
{
    class Program
    {

        static bool Noted = false;
        static bool exit = false;
        static bool DoDownload = false;

        public static void Main(string[] args)
        {
            Console.WriteLine(" ");
            Console.WriteLine("Welcome to CerebraCore Tumblr DL");
            Console.WriteLine("**********************************");
            Console.WriteLine("************* 2016 ***************");
            Console.WriteLine("**********************************");

            if (InitialiseDB())
            {
                Noted = false;
                exit = false;
                DoDownload = false;
            }
            else
            {
                exit = true;
                DoDownload = false;
            }

            
            while (!exit)
            {
                if (!DoDownload)
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
                
                    Console.WriteLine("Checking for downloads");
                    int HasDownloads = Global.mod.tDownloads.Where(ep => ep.Downloaded == "false" && ep.PostLinkID != null).Count();
                    
                    if (!Noted && HasDownloads == 0)
                    {
                        Console.WriteLine(HasDownloads.ToString() + " downloads found");
                        ShowMenue();
                        Noted = true;
                    }

                    if (DoDownload && HasDownloads > 0)
                    {
                        Process();
                    }
                    else
                    {
                        Noted = false;
                    }
                

                if (Console.KeyAvailable)
                {
                    String x = Console.ReadLine();
                    processCMD(x);
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message + ": Would you like to Exit?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    exit = true;

            }
        }

        static void Process()
        {
            Console.WriteLine("Downloading 10.....");

            List<tDownload> dlist = Global.mod.tDownloads.Where(ep => ep.Downloaded == "false" && ep.PostLinkID != null).Take(10).ToList();

            Parallel.ForEach(
                dlist,
                new ParallelOptions { MaxDegreeOfParallelism = dlist.Count },
                DownloadFile);

            foreach (tDownload xxx in dlist)
            {
                TumblrPost ps = Global.mod.TumblrPosts.Where(ep => ep.PostLinkID == xxx.PostLinkID).FirstOrDefault();
                if (ps != null)
                {
                    ps.Downloaded = xxx.Downloaded;
                }
            }

            
            Global.mod.SaveChanges();
            Noted = false;
        }

        static void ShowMenue()
        {
            Console.WriteLine("");
            Console.WriteLine("Commands Avalible");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(" Start   - Starts the Download.");
            Console.WriteLine(" Stop    - Stops the Download.");
            Console.WriteLine(" Exit    - Exits the application.");
            Console.WriteLine(" Config  - Opens the configuration screen.");
            Console.WriteLine(" chk     - Validates the downloaded files.");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("What would you like to do?");
            string x = Console.ReadLine();
            processCMD(x);
        }

        static void processCMD(String input)
        {
            string[] xarray = input.Split(',');
            if (xarray[0].ToLower() == "exit")
            {
                exit = true;
            }
            else if (xarray[0].ToLower() == "config")
            {
                frmConfiguration configure = new frmConfiguration();
                configure.ShowDialog();

                Console.WriteLine("Configuration Saved.");
            }
            else if (xarray[0].ToLower() == "start")
            {
                DoDownload = true;

                Console.WriteLine("Download Started");
            }
            else if (xarray[0].ToLower() == "stop")
            {
                DoDownload = false;

                Console.WriteLine("No Download.");
            }
            else if (xarray[0].ToLower() == "chk")
            {
                foreach (tDownload xo in Global.mod.tDownloads.Where(ep => ep.Downloaded == "true" && ep.PostLinkID != null))
                {
                    if (!File.Exists(xo.SystemPath))
                    {
                        xo.Downloaded = "false";
                    }
                }
                Global.mod.SaveChanges();

                Console.WriteLine("Check done.");
            }
            else
            {

                Console.WriteLine("Unknown Command");
            }
        }







        public static void DownloadFile(tDownload url)
        {
            url.Downloaded = (Download(url.URL, url.SystemPath)).ToLower();
            
        }

        static string Download(String URLX, String FilePath)
        {
            Boolean Retry = true;
            bool stat = false;
            int FailCount = 0;

            while (Retry)
            {
                try
                {
                    String fileName = URLX.Substring(URLX.LastIndexOf(("/")) + 1);

                    if (File.Exists(FilePath))
                    {
                        Retry = false;
                        return "true";
                    }

                    if (!Directory.Exists(FilePath.Substring(0, FilePath.LastIndexOf(("\\")))))
                    {
                        Directory.CreateDirectory(FilePath.Substring(0, FilePath.LastIndexOf(("\\"))));
                    }

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(URLX, FilePath);
                        stat = true;
                        Retry = false;
                    }
                }
                catch (Exception ex)
                {
                    FailCount++;
                    stat = false;
                    Retry = false;
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                }
            }
            return stat.ToString();
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

        
    }
}
