using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerebraCoreTumblr.DataAccess.Jason;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using CerebraCoreTumblr.DataAccess.DTO;

namespace CerebraCoreTumblr.DataAccess
{
    public class Global
    {
       // public static TumblrModel mod = new TumblrModel();

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
            return null;// mod.TumblrTags.Where(ep => ep.name == name).FirstOrDefault() ?? new TumblrTag { id = -1, name = name };
        }

        public static TumblrBlog newBlog(jsRootObject jsobj, TumblrAccount currentaccount)
        {
            TumblrBlog newB = null;// mod.tBlogs.Where(ep => ep.BlogName == jsobj.response.blog.name).FirstOrDefault() ?? new tBlog { id = -1 };
            //if (newB.id > -1)
            //{
            //    newB.BlogName = jsobj.response.blog.name;
            //    newB.DoSync = "true";
            //    newB.Limit = 10;
            //    newB.Offset = jsobj.response.blog.posts;
            //    newB.RequestURL = "";
            //    newB.Synced = 0;
            //    newB.tAccountID = currentaccount.id;
            //    newB.Total = jsobj.response.blog.posts;
            //    newB.BlogSystemPath = currentaccount.AccountSystemPath + "\\" + newB.BlogName;
            //    newB.BlogWebPath = "";
            //}
            return newB;
        }
    }
}
