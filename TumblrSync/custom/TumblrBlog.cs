using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TumblrSync.Objects.Jason;

namespace TumblrSync
{
    public partial class TumblrBlog
    {
        public void UpdateData(jsBlog jsb)
        {
            title = jsb.title ?? "";
            name = jsb.name ?? "";
            posts = jsb.posts;
            url = jsb.url ?? "";
            updated = jsb.updated;
            description = jsb.description ?? "";
            is_nsfw = Convert.ToInt32(jsb.is_nsfw);
            ask = Convert.ToInt32(jsb.ask);
            ask_page_title = jsb.ask_page_title ?? "";
            ask_anon = Convert.ToInt32(jsb.ask_anon);
            submission_page_title = jsb.submission_page_title ?? "";
            share_likes = Convert.ToInt32(jsb.share_likes);
        }

        public void AddPosts(List<jsPost> jsps, tBlog CurrentBlog)
        {
            foreach (jsPost ps in jsps)
            {
                TumblrPost post = new TumblrPost();
                post.blog_name = ps.blog_name ?? "";
                post.caption = ps.caption ?? "";
                post.date = ps.date;
                post.format = ps.format;
                post.highlighted = "";
                post.image_permalink = ps.image_permalink ?? "";
                post.link_url = ps.link_url ?? "";
                post.note_count = ps.note_count;
                post.post_url = ps.post_url ?? "";
                post.reblog_key = ps.reblog_key ?? "";
                post.short_url = ps.short_url ?? "";
                post.slug = ps.slug ?? "";
                post.source_title = ps.source_title ?? "";
                post.source_url = ps.source_url ?? "";
                post.state = ps.state ?? "";
                post.type = ps.type ?? "";
                post.Deleted = "false";
                post.tumblr_id = ps.id.ToString();
                //post.timestamp;
                //post.tbPostOtherMedias;

                if (post.type == "photo")
                {
                    post.Downloaded = "false";
                }
                else
                {
                    post.Downloaded = "na";
                }

                #region Photos
                if (ps.photos != null)
                {
                    foreach (jsPhoto photo in ps.photos)
                    {
                        TumblrPhoto newphoto = new TumblrPhoto();
                        newphoto.caption = photo.caption;
                        newphoto.Downloaded = "false";
                        foreach (jsAltSize alt in photo.alt_sizes)
                        {
                            TumblrAltSize atls = new TumblrAltSize();
                            atls.height = alt.height;
                            atls.tbPhotoId = newphoto.Id;
                            atls.url = alt.url;
                            atls.width = alt.width;
                            atls.LocalURL = "na";
                            atls.Downloaded = "false";
                            newphoto.TumblrAltSizes.Add(atls);
                        }

                        TumblrOriginalSize tbogs = new TumblrOriginalSize();
                        tbogs.height = photo.original_size.height;
                        tbogs.url = photo.original_size.url;
                        tbogs.width = photo.original_size.width;
                        tbogs.tbPhotoId = newphoto.Id;
                        //tbogs.LocalURL = "~/Content/Website/Images/Media/Account/MyPrivate/" + blog.TumblrAccount.tb_username + "/Blogs/" + blog.name + "/OriginalSize/" + tbogs.url.Substring(tbogs.url.LastIndexOf(("/")) + 1);

                        tbogs.Downloaded = "false";
                        newphoto.TumblrOriginalSizes.Add(tbogs);
                        Guid x = Guid.NewGuid();
                        tDownload newDownload = new tDownload();
                        newDownload.Downloaded = "false";
                        newDownload.Filename = photo.original_size.url.Substring(photo.original_size.url.LastIndexOf(("/")) + 1);
                        newDownload.jsPostID = post.id;

                        newDownload.tBlogID = CurrentBlog.id;
                        newDownload.URL = photo.original_size.url;
                        newDownload.WebPath = ConfigurationManager.AppSettings["VirDir"] + CurrentBlog.tAccount.username + "/" + CurrentBlog.BlogName + "/" + newDownload.Filename;
                        newDownload.SystemPath = CurrentBlog.BlogSystemPath + "\\" + newDownload.Filename;
                        newDownload.PostLinkID = x;
                        Global.mod.tDownloads.Add(newDownload);
                        tbogs.SystemPath = newDownload.SystemPath;
                        tbogs.LocalURL = newDownload.WebPath;

                        

                        post.PostLinkID = x;
                        post.TumblrPhotos.Add(newphoto);
                        newphoto.tbPostId = post.id;
                    }
                }
                #endregion

                #region Video


                post.summary = ps.summary ?? "";
                post.recommended_source = (string)ps.recommended_source ?? "";
                post.recommended_color = (string)ps.recommended_color ?? "";
                post.trail = "";
                post.video_url = ps.video_url ?? "";
                post.html5_capable = ps.html5_capable.ToString().ToLower();
                post.thumbnail_url = ps.thumbnail_url ?? "";
                post.thumbnail_width = ps.thumbnail_width ?? "";
                post.thumbnail_height = ps.thumbnail_height ?? "";
                post.duration = ps.duration ?? "";

                if (ps.type == "video")
                {

                    if (ps.player != null && ps.player.Count > 0)
                    {
                        foreach (jsPlayer p in ps.player)
                        {
                            TumblrPlayer pl = new TumblrPlayer();
                            pl.embed_code = p.embed_code;
                            pl.width = p.width.ToString();
                            post.TumblrPlayers.Add(pl);
                        }
                    }
                }
                post.video_type = ps.video_type ?? "";


                #endregion

                #region Reblog
                if (ps.reblog != null)
                {
                    TumblrReblog rBlog = new TumblrReblog();
                    rBlog.comment = "";
                    rBlog.tree_html = "";
                    post.TumblrReblogs.Add(rBlog);
                }
                #endregion

                #region Tags
                if (ps.tags.Count > 0)
                {
                    post.TumblrTags.Clear();
                    String tagstags = "";
                    foreach (string tag in ps.tags)
                    {
                        tagstags += "," + tag.Replace(' ', '_');
                    }
                    string[] tagNames = tagstags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String tagName in tagNames)
                    {
                        TumblrTag x = Global.getTag(tagName);
                        if (x.id < 0)
                        {
                            x.name = tagName;
                        }
                        post.TumblrTags.Add(x);
                    }
                }

                #endregion

                TumblrPosts.Add(post);
            }
        }
    }
}
