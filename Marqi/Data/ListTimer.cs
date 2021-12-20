using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Marqi.Data
{
    public class ListTimer<T> : IDataSource<T>
    {
        private readonly ILogger _logger;
        private readonly IDataSource<List<T>> _source;
        private readonly int _interval;
        private readonly Timer _timer;
        private List<T> _list;
        private int _next;

        public ListTimer(ILogger<ListTimer<T>> logger, IDataSource<List<T>> source, int interval)
        {
            _logger = logger;
            _source = source;
            _source.Update = SourceUpdate;
            _interval = interval;
            _timer = new Timer(NextItem, null, -1, 0);
            _next = 0;
        }

        public Action<T> Update { get; set; }

        public async Task Refresh()
        {
            await _source.Refresh();
        }

        private void SourceUpdate(List<T> list)
        {
            _list = list;
            if(_list != null && _list.Count > 0)
            {
                _timer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(_interval));
            }
            else
            {
                Update(default(T));
                _timer.Change(-1, 0);
            }
        }

        private void NextItem(object state)
        {
            if(_next >= _list.Count)
            {
                _next = 0;
            }

            Update(_list[_next]);
            _next++;
        }
    }
}
