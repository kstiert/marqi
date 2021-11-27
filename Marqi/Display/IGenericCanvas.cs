namespace Marqi.Display
{
    public interface IGenericCanvas : ICanvas
    {
        int Width { get; }

        int Height { get; }

        void Swap();
    }
}