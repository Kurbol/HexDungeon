using System;
using System.Collections.Generic;

[Serializable]
public struct HexCell<T>
{
    public IHexCoordinate Coordinate { get; private set; }
    public T Tile { get; set; }

    public HexCell(IHexCoordinate coordinate, T tile)
    {
        Coordinate = coordinate;
        Tile = tile;
    }

    public HexCell(KeyValuePair<IHexCoordinate, T> keyValuePair)
        : this(keyValuePair.Key, keyValuePair.Value)
    {
    }
}