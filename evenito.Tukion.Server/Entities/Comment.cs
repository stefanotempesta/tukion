using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Server.Entities
{
    public class Comment
    {
        public Guid UserId { get; set; }

        public Guid VideoId { get; set; }

        public DateTime AddedOn { get; set; }

        public string Text { get; set; }
    }
}
