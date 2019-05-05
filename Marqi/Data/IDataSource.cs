using System;

namespace Marqi.Data
{
    interface IDataSource<T>
    {
        Action<T> Update { get; set; }
    }
}
