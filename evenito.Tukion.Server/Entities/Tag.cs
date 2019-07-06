using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Server.Entities
{
    public class Tag : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
