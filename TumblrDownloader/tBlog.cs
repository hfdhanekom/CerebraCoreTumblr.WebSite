//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TumblrDownloader
{
    using System;
    using System.Collections.Generic;
    
    public partial class tBlog
    {
        public int id { get; set; }
        public int tAccountID { get; set; }
        public string BlogName { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Synced { get; set; }
        public string DoSync { get; set; }
        public string RequestURL { get; set; }
        public string BlogSystemPath { get; set; }
        public string BlogWebPath { get; set; }
    
        public virtual tAccount tAccount { get; set; }
    }
}
