public interface IHexCoordinate
{
    int X { get; }

    int Y { get; }

    int Z { get; }

    void Move(HexDirection direction, int distance);
}