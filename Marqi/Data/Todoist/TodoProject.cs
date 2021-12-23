using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Todoist.Net;

namespace Marqi.Data.Todoist
{
    public class TodoProject : IDataSource<List<Todo>>
    {
        private readonly ILogger _logger;

        private readonly ITodoistClient _client;

        public TodoProject(ILogger<TodoProject> logger, IOptions<TodoOptions> options)
        {
            _logger = logger;
            _client = new TodoistClient(options.Value.Token);
        }

        public string Cron => "*/1 * * * *";

        public Action<List<Todo>> Update { get; set; }

        public async Task Refresh()
        {
            _logger.LogDebug("Refreshing Todoist");
            var items = await _client.Items.GetAsync();
            Update(items.Where(i => !(i.IsChecked ?? true)).Select(i => new Todo { Name = i.Content }).ToList());
            _logger.LogDebug("Completed refreshing Todoist");
        }
    }
}
