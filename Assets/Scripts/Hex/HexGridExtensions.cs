using UnityEngine;

public static class HexGridExtensions
{
    public static IHexCoordinate GetHexCoordinateFromWorldPosition(this HexGrid hexGrid, Vector3 worldPosition)
    {
        return hexGrid.GetHexCoordinateFromLocalPosition(hexGrid.GetLocalPosition(worldPosition));
    }

    public static IHexTile GetHexTileFromWorldPosition(this HexGrid hexGrid, Vector3 worldPosition)
    {
        return hexGrid.GetHexTileFromLocalPosition(hexGrid.GetLocalPosition(worldPosition));
    }

    public static Vector3 GetLocalPosition(this HexGrid hexGrid, Vector3 worldPosition)
    {
        return hexGrid.transform.InverseTransformPoint(worldPosition);
    }
}