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

        public static TumblrModel mod = new TumblrModel();

        public static void Main(string[] args)
        {
            Console.WriteLine(" ");
            Console.WriteLine("Welcome to CerebraCore Tumblr Download");
            Console.WriteLine("**********************************");
            Console.WriteLine("************* 2016 ***************");
            Console.WriteLine("**********************************");
            Console.WriteLine("Commands Avalible");
            Console.WriteLine(" ");
            Console.WriteLine(" Start   - Starts the Download.");
            Console.WriteLine(" Stop    - Stops the Download.");
            Console.WriteLine(" Exit    - Exits the application.");
            Console.WriteLine(" Config  - Opens the configuration screen.");
            Console.WriteLine(" chk     - Validates the downloaded files.");
            Console.WriteLine("");
            Console.WriteLine("***********************************");
            Console.WriteLine("What would you like to do?");

            
            bool Noted = false;

            //Update Blog
            bool exit = false;
            bool DoDownload = false;
            
            while (!exit)
            {
                try
                {
                    if (DoDownload)
                    {
                        
                        Console.WriteLine("Checking for downloads");
                        int HasDownloads = mod.tDownloads.Where(ep => ep.Downloaded == "false" && ep.PostLinkID != null).Count();
                        if (!Noted)
                        {
                            
                            Console.WriteLine(HasDownloads.ToString() + " downloads found");
                            Noted = true;
                        }
                        if (HasDownloads > 0)
                        {

                            
                            Console.WriteLine("Downloading 10.....");

                            List <tDownload> dlist = mod.tDownloads.Where(ep => ep.Downloaded == "false" && ep.PostLinkID != null).Take(10).ToList(); 

                            Parallel.ForEach(
                                dlist,
                                new ParallelOptions { MaxDegreeOfParallelism = dlist.Count },
                                DownloadFile);

                            foreach (tDownload xxx in dlist)
                            {
                                TumblrPost ps = mod.TumblrPosts.Where(ep => ep.PostLinkID == xxx.PostLinkID).FirstOrDefault();
                                if (ps != null)
                                {
                                    ps.Downloaded = xxx.Downloaded;
                                }
                            }

                            mod.SaveChanges();
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

                    if (Console.KeyAvailable)
                    {
                        String x = Console.ReadLine();
                        string[] xarray = x.Split(',');
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
                            foreach (tDownload xo in mod.tDownloads.Where(ep => ep.Downloaded == "true" && ep.PostLinkID != null))
                            {
                                if (!File.Exists(xo.SystemPath))
                                {
                                    xo.Downloaded = "false";
                                }
                            }
                            mod.SaveChanges();
                            
                            Console.WriteLine("Check done.");
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

                }
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
}
