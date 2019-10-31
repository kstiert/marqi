using System;
using System.Threading.Tasks;

namespace Marqi.Data
{
    public interface IDataSource<T>
    {
        Action<T> Update { get; set; }

         Task Refresh();
    }
}
