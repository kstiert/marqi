using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marqi.Widgets
{
    public interface IWidgetFactory
    {
        Task<IEnumerable<IWidget>> MakeWidgets();
    }
}