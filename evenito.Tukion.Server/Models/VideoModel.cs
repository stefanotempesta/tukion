using evenito.Tukion.Server.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Server.Models
{
    public class VideoModel
    {
        public Video Video { get; set; }

        public User Owner { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public IEnumerable<View> Views { get; set; }

        public IEnumerable<Reaction> Reactions { get; set; }

        public IEnumerable<Favourite> Favourites { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Channel> Channels { get; set; }

        // Format duration as HH:MM:SS
        public string DurationString => $"{(Video.Duration / 3600):00}:{(Video.Duration % 3600 / 60):00}:{(Video.Duration % 3600 % 60):00}";
    }
}
