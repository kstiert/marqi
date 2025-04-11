using System.Collections.Generic;
using System.Threading.Tasks;
using Marqi.Attributes;
using Microsoft.Extensions.Options;

namespace Marqi.Widgets.Factory.Json
{
    [WidgetFactory("json")]
    public class JsonWidgetFactory : IWidgetFactory
    {

        public JsonWidgetFactory(IOptions<WidgetFactoryOptions> options)
        {
            
        }


        public Task<IEnumerable<IWidget>> MakeWidgets()
        {
            throw new System.NotImplementedException();
        }
    }
}