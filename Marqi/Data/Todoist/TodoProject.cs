using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Todoist.Net;

namespace Marqi.Data.Todoist
{
    public class TodoProject : IDataSource<List<Todo>>
    {
        private readonly ITodoistClient _client;

        public TodoProject(IOptions<TodoOptions> options)
        {
            _client = new TodoistClient(options.Value.Token);
        }

        public Action<List<Todo>> Update { get; set; }

        public async Task Refresh()
        {
            var items = await _client.Items.GetAsync();
            Update(items.Select(i => new Todo { Name = i.Content }).ToList());
        }
    }
}
