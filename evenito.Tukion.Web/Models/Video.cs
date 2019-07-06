using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Web.Models
{
    public class Video
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public Uri ThumbnailURL { get; set; }
    }
}
