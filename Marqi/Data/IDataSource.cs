using System;

namespace Marqi.Data
{
    public interface IDataSource<T>
    {
        Action<T> Update { get; set; }

        void Refresh();
    }
}
