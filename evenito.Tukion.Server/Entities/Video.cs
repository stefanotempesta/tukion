using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Server.Entities
{
    public class Video : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; }   // in seconds

        public Uri ContentURL { get; set; }

        public Uri ThumbnailURL { get; set; }

        public VideoVisibility Visibility { get; set; }

        public DateTime AddedOn { get; set; }

        public Guid OwnerId { get; set; }
    }
}
