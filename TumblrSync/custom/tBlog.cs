using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TumblrSync.Objects.Jason;

namespace TumblrSync
{
    public partial class tBlog
    {
        public bool CanSync()
        {
            if (Total != -1 && Synced != -1 && Offset != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetRequest()
        {
            return "http://api.tumblr.com/v2/blog/" + BlogName + ".tumblr.com/posts?api_key=" + tAccount.ConsumerKey + "&offset=[offset]&limit=[limit]".Replace("[offset]", Offset.ToString()).Replace("[limit]", Limit.ToString());
        }

        string GetRequest(int offset, int limit)
        {
            return "http://api.tumblr.com/v2/blog/" + BlogName + ".tumblr.com/posts?api_key=" + tAccount.ConsumerKey + "&offset=[offset]&limit=[limit]".Replace("[offset]", offset.ToString()).Replace("[limit]", limit.ToString());
        }

        public void UpdateBlog()
        {
            String Data = DoRequest(GetRequest(1, 1));
            jsRootObject obj = ToJson(Data);

            if (Total != obj.response.blog.posts)
            {
                Int32 PostAdjustments = 0;
                PostAdjustments = obj.response.blog.posts - Total;
                Total = obj.response.blog.posts;
                Offset = Offset + PostAdjustments;
            }
        }

        String DoRequest(String URL)
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

        jsRootObject ToJson(String data)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(jsRootObject));
            jsRootObject obj = null;
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                obj = (jsRootObject)ser.ReadObject(stream);
            }
            return obj;
        }

        Int32 DumOffset;
        Int32 DumSynced;
        String DumDoSunc;

        void Calculate()
        {
            DumOffset = Offset;
            DumSynced = Synced;
            DumDoSunc = DoSync;

            Offset = (Total - Synced) - Limit;
            if (Offset <= 0)
            {
                Offset = 0;
                Limit = (Total - Synced);
                DoSync = "false";
            }
            Synced = Synced + Limit;
        }

        void UndoCalculate()
        {
            Offset = DumOffset;
            Synced = DumSynced;
            DoSync = DumDoSunc;
        }

        public void SyncPosts()
        {
            Calculate();
            try
            {
                //Step4 Get Posts

                String Data = DoRequest(GetRequest());
                jsRootObject obj = ToJson(Data);

                //Step5 Convert to TumblrBlog
                TumblrBlog currentBlog = Global.mod.TumblrBlogs.Where(ep => ep.name == BlogName).FirstOrDefault() ?? new TumblrBlog { Id = -1 };
                currentBlog.UpdateData(obj.response.blog);
                currentBlog.AddPosts(obj.response.posts, this);

                if (currentBlog.Id == -1)
                {
                    Global.mod.TumblrBlogs.Add(currentBlog);
                }

                //Step6 Saves EveryThing
                Global.mod.SaveChanges();
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
                UndoCalculate();
            }
        }
    }
}
