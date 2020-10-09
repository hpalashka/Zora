using System;
using System.Collections.Generic;

namespace Zora.Shared.Domain
{
    public interface IInitialData
    {
        Type EntityType { get; }

        IEnumerable<object> GetData();
    }
}
