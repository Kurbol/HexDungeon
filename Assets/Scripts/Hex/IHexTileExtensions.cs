using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IHexTileExtensions
{
    public static IDictionary<IHexCoordinate, IHexTile> Neighbors(this IDictionary<IHexCoordinate, IHexTile> tiles, IHexCoordinate hexCoordinate)
    {
        var neighbors = new Dictionary<IHexCoordinate, IHexTile>();

        IEnumerable<IHexCoordinate> neighborCoordinates = hexCoordinate.Neighbors();
        foreach(IHexCoordinate neighborCoordinate in neighborCoordinates)
        {
            IHexTile neighbor = null;
            if (tiles.TryGetValue(neighborCoordinate, out neighbor))
                neighbors.Add(neighborCoordinate, neighbor);
        }

        return neighbors;
    }

    public static IEnumerable<Triangle> TriangulateBridge(this IDictionary<IHexCoordinate, IHexTile> neighbors, IHexTile tile, HexDirection direction)
    {
        if (neighbors == null || !neighbors.Any() || tile == null)
            yield break;

        IHexTile neighbor1 = null;
        if (!neighbors.TryGetValue(tile.HexCoordinate.Neighbor(direction), out neighbor1))
            yield break;

        Vector3 tileCenter = tile.HexCoordinate.ToLocalPosition(tile.HexMetrics);
        var tileInnerCorners = tile.HexMetrics.InnerCorners(direction);

        Vector3 neighbor1Center = neighbor1.HexCoordinate.ToLocalPosition(neighbor1.HexMetrics);
        var neighbor1InnerCorners = neighbor1.HexMetrics.InnerCorners(direction.Next(3));

        yield return new Triangle
        (
            tileCenter + tileInnerCorners[0],
            neighbor1Center + neighbor1InnerCorners[1],
            tileCenter + tileInnerCorners[1],
            tile.Color,
            neighbor1.Color,
            tile.Color
        );

        yield return new Triangle
        (
            tileCenter + tileInnerCorners[1],
            neighbor1Center + neighbor1InnerCorners[1],
            neighbor1Center + neighbor1InnerCorners[0],
            tile.Color,
            neighbor1.Color,
            neighbor1.Color
        );

        IHexTile neighbor2 = null;
        if (!neighbors.TryGetValue(tile.HexCoordinate.Neighbor(direction.Next()), out neighbor2))
            yield break;

        Vector3 neighbor2Center = neighbor2.HexCoordinate.ToLocalPosition(neighbor2.HexMetrics);
        var neighbor2InnerCorners = neighbor2.HexMetrics.InnerCorners(direction.Next(4));

        yield return new Triangle
        (
            tileCenter + tileInnerCorners[1],
            neighbor1Center + neighbor1InnerCorners[0],
            neighbor2Center + neighbor2InnerCorners[1],
            tile.Color,
            neighbor1.Color,
            neighbor2.Color
        );
    }
}
