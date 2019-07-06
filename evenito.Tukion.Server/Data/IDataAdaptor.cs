using System;
using System.Collections.Generic;
using System.Text;

namespace evenito.Tukion.Server.Data
{
    public interface IDataAdaptor<T> : IDisposable where T : new()
    {
        IEnumerable<T> LoadAll();

        T Load(Guid id);
    }
}
