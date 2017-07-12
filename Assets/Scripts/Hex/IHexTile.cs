using UnityEngine;

public interface IHexTile
{
    HexCoordinate HexCoordinate { get; }
    HexMetrics HexMetrics { get; }
    Color Color { get; set; }
}
