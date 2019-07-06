using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Server.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
