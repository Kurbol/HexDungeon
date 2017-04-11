using System.Collections.Generic;

public interface IHexGrid<T>
{
    float Scale { get; }

    HexMetrics HexMetrics { get; }

    Dictionary<IHexCoordinate, T> HexMap { get; }
}