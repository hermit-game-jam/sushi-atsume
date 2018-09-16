namespace Sushi
{
    public interface ISushiState
    {
        bool AutoMovable { get; }
        bool Rotatable { get; }
        void OnClick();
    }
}