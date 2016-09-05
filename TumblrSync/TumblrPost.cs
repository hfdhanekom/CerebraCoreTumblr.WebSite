//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TumblrSync
{
    using System;
    using System.Collections.Generic;
    
    public partial class TumblrPost
    {
        public TumblrPost()
        {
            this.TumblrPhotos = new HashSet<TumblrPhoto>();
            this.TumblrPlayers = new HashSet<TumblrPlayer>();
            this.TumblrPostComments = new HashSet<TumblrPostComment>();
            this.TumblrPostOtherMedias = new HashSet<TumblrPostOtherMedia>();
            this.TumblrReblogs = new HashSet<TumblrReblog>();
            this.TumblrTags = new HashSet<TumblrTag>();
        }
    
        public int id { get; set; }
        public int Blogid { get; set; }
        public string blog_name { get; set; }
        public string tumblr_id { get; set; }
        public string post_url { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public string date { get; set; }
        public int timestamp { get; set; }
        public string state { get; set; }
        public string format { get; set; }
        public string reblog_key { get; set; }
        public string short_url { get; set; }
        public string highlighted { get; set; }
        public int note_count { get; set; }
        public string source_url { get; set; }
        public string source_title { get; set; }
        public string caption { get; set; }
        public string image_permalink { get; set; }
        public string link_url { get; set; }
        public string summary { get; set; }
        public string recommended_source { get; set; }
        public string recommended_color { get; set; }
        public string trail { get; set; }
        public string video_url { get; set; }
        public string html5_capable { get; set; }
        public string thumbnail_url { get; set; }
        public string thumbnail_width { get; set; }
        public string thumbnail_height { get; set; }
        public string duration { get; set; }
        public string video_type { get; set; }
        public string Downloaded { get; set; }
        public string Deleted { get; set; }
        public string Fav { get; set; }
        public Nullable<System.DateTime> DateSynced { get; set; }
        public string Viewed { get; set; }
        public Nullable<System.Guid> PostLinkID { get; set; }
    
        public virtual TumblrBlog TumblrBlog { get; set; }
        public virtual ICollection<TumblrPhoto> TumblrPhotos { get; set; }
        public virtual ICollection<TumblrPlayer> TumblrPlayers { get; set; }
        public virtual ICollection<TumblrPostComment> TumblrPostComments { get; set; }
        public virtual ICollection<TumblrPostOtherMedia> TumblrPostOtherMedias { get; set; }
        public virtual ICollection<TumblrReblog> TumblrReblogs { get; set; }
        public virtual ICollection<TumblrTag> TumblrTags { get; set; }
    }
}
