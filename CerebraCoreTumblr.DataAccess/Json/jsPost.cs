using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerebraCoreTumblr.DataAccess.Jason
{
   public class jsPost
    {
        public string blog_name { get; set; }
        public object id { get; set; }
        public string post_url { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public string date { get; set; }
        public int timestamp { get; set; }
        public string state { get; set; }
        public string format { get; set; }
        public string reblog_key { get; set; }
        public List<string> tags { get; set; }
        public string short_url { get; set; }
        public string summary { get; set; }
        public object recommended_source { get; set; }
        public object recommended_color { get; set; }
        public List<object> highlighted { get; set; }
        public int note_count { get; set; }

        public string source_url { get; set; }
        public string source_title { get; set; }
        public string caption { get; set; }
        public jsReblog reblog { get; set; }
        public string image_permalink { get; set; }
        public List<jsPhoto> photos { get; set; }
        public string link_url { get; set; }

   

        public List<jsTrail> trail { get; set; }
        public string video_url { get; set; }
        public bool html5_capable { get; set; }
        public string thumbnail_url { get; set; }
        public string thumbnail_width { get; set; }
        public string thumbnail_height { get; set; }
        public string duration { get; set; }
        public List<jsPlayer> player{get;set; }
        public string xplayer { get; set; }
        public string video_type { get; set; }

        public string title { get; set; }
        public string body { get; set; }
        public string artist { get; set; }
        public string year { get; set; }
        public string track_name { get; set; }
        public string embed { get; set; }
        public string plays { get; set; }
        public string audio_url { get; set; }
        public string audio_source_url { get; set; }
        public string audio_type { get; set; }
        public string photoset_layout { get; set; }

        public string asking_name { get; set; }
        public string asking_url { get; set; }
        public string question { get; set; }
        public string answer { get; set; }


       
    }
}
