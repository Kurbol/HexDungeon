using System.Collections.Generic;

public interface IHexGrid
{
    int Size { get; }

    Dictionary<IHexCoordinate, IHexTile> HexTiles { get; }
}