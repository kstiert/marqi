namespace Marqi.Common.Display
{
    public interface IGenericCanvas : ICanvas
    {
        int Width { get; }

        int Height { get; }

        void Swap();
    }
}