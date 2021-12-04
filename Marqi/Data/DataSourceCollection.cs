using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marqi.Data
{
    public class DataSourceCollection
    {
        void GetThing<T>(string thing)
        {
            var data = Expression.Parameter(typeof(T), "data");
            var g = Expression.Lambda<Func<T, string>>(Expression.PropertyOrField(data, thing), data).Compile();
        }
    }
}
