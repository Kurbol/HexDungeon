public interface IHexCoordinate
{
    int X { get; }

    int Y { get; }

    int Z { get; }

    void AddX(int value);

    void AddY(int value);

    void AddZ(int value);
}