using System.Linq;
using UnityEngine;

public static class HexGridExtensions
{
    public static HexMetrics GetHexMetrics(this HexGrid hexRoom)
    {
        if (hexRoom == null || !hexRoom.HexTiles.Any())
            return new HexMetrics();

        IHexTile firstTile = hexRoom.HexTiles.First().Value;
        if (firstTile != null)
            return new HexMetrics();

        return firstTile.HexMetrics;
    }

    public static IHexCoordinate GetHexCoordinateFromWorldPosition(this HexGrid hexRoom, Vector3 worldPosition)
    {
        if (hexRoom == null || !hexRoom.HexTiles.Any())
            return null;

        Vector3 localPosition = hexRoom.transform.InverseTransformPoint(worldPosition);
        return localPosition.ToHexCoordinate(GetHexMetrics(hexRoom));
    }

    public static IHexTile GetHexTileFromWorldPosition(this HexGrid hexRoom, Vector3 worldPosition)
    {
        IHexCoordinate coordinate = GetHexCoordinateFromWorldPosition(hexRoom, worldPosition);
        if (!hexRoom.HexTiles.ContainsKey(coordinate))
            return null;

        return hexRoom.HexTiles[coordinate];
    }

    public void ColorTile(Vector3 worldPosition, Color color)
    {
        IHexTile hexTile = GetHexTileFromWorldPosition(this, worldPosition);
        if (hexTile == null)
            return;

        hexTile.Color = color;
        Debug.Log("touched at " + hexTile.HexCoordinate.ToString());

        if (CellClicked != null)
            CellClicked.Invoke();
    }
}
