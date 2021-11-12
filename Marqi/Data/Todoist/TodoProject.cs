using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todoist.Net;

namespace Marqi.Data.Todoist
{
    public class TodoProject : IDataSource<List<Todo>>
    {
        private readonly ITodoistClient _client;

        public TodoProject()
        {
            _client = new TodoistClient("62575f21e12cb64ef291e8716f63c8edc117000a");
        }

        public Action<List<Todo>> Update { get; set; }

        public async Task Refresh()
        {
            var items = await _client.Items.GetAsync();
            Update(items.Select(i => new Todo { Name = i.Content }).ToList());
        }
    }
}
