using System;
using System.Threading.Tasks;

namespace Marqi.Data
{
    public interface IDataSource<T> : IDataSource
    {
        Action<T> Update { get; set; } 
    }

    public interface IDataSource
    {
        string Cron { get; }

        Task Refresh();
    }
}
