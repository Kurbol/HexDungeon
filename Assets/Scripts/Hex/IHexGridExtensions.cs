using System.Linq;
using UnityEngine;

public static class IHexGridExtensions
{
    public static HexMetrics GetHexMetrics(this IHexGrid hexGrid)
    {
        if (hexGrid == null || !hexGrid.HexTiles.Any())
            return new HexMetrics();

        IHexTile firstTile = hexGrid.HexTiles.First().Value;
        if (firstTile == null)
            return new HexMetrics();

        return firstTile.HexMetrics;
    }

    public static IHexCoordinate GetHexCoordinateFromLocalPosition(this IHexGrid hexGrid, Vector3 localPosition)
    {
        if (hexGrid == null || !hexGrid.HexTiles.Any())
            return null;

        return localPosition.ToHexCoordinate(GetHexMetrics(hexGrid));
    }

    public static IHexTile GetHexTileFromLocalPosition(this IHexGrid hexGrid, Vector3 localPosition)
    {
        IHexCoordinate coordinate = hexGrid.GetHexCoordinateFromLocalPosition(localPosition);
        if (!hexGrid.HexTiles.ContainsKey(coordinate))
            return null;

        return hexGrid.HexTiles[coordinate];
    }
}