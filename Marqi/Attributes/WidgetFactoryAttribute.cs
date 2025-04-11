using System;

namespace Marqi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class WidgetFactoryAttribute : Attribute
    {
        readonly string _name;

        public WidgetFactoryAttribute(string name)
        {
            _name = name;
        }
        
        public string Name
        {
            get { return _name; }
        }
    }
}