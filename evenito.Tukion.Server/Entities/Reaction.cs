using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Server.Entities
{
    public class Reaction
    {
        public Guid UserId { get; set; }

        public Guid VideoId { get; set; }

        public DateTime AddedOn { get; set; }

        public ReactionType Type { get; set; }
    }
}
