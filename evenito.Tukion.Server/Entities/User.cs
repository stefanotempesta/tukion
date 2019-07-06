using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Server.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}
