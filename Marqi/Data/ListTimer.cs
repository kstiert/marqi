﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Marqi.Data
{
    public class ListTimer<T> : IDataSource<T>
    {
        private readonly IDataSource<List<T>> _source;
        private readonly int _interval;
        private readonly Timer _timer;
        private List<T> _list;
        private int _next;

        public ListTimer(IDataSource<List<T>> source, int interval)
        {
            _source = source;
            _source.Update = SourceUpdate;
            _interval = interval;
            _timer = new Timer(NextItem, null, -1, 0);
            _next = 0;
        }

        public Action<T> Update { get; set; }

        public void Refresh()
        {
            _source.Refresh();
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
