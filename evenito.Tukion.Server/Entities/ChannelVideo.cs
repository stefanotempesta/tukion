using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Server.Entities
{
    public class ChannelVideo
    {
        public Guid ChannelId { get; set; }

        public Guid VideoId { get; set; }

        public DateTime AddedOn { get; set; }

        public int DisplaySequence { get; set; }
    }
}
