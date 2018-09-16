namespace Sushi
{
    public interface ISushiState
    {
        bool AutoMovable { get; }
        void OnClick();
    }
}