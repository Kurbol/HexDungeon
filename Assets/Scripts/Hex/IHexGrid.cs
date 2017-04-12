using System.Collections.Generic;

public interface IHexGrid<T>
{
    int Size { get; }

    float Scale { get; }

    HexMetrics HexMetrics { get; }

    Dictionary<IHexCoordinate, T> HexMap { get; }
}