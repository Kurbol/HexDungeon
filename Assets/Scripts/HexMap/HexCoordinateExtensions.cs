using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public static class HexCoordinateExtensions
{
    public static Vector3 ToLocalPosition(this IHexCoordinate hexCoordinate, HexMetrics hexMetrics)
    {
        return hexCoordinate.ToLocalPosition(hexMetrics, 1);
    }

    public static Vector3 ToLocalPosition(this IHexCoordinate hexCoordinate, HexMetrics hexMetrics, float scale)
    {
        var localPosition = new Vector3();

        if (scale <= 0)
            return localPosition;

        if (hexMetrics.Orientation == HexOrientation.FlatUp)
        {
            localPosition.x = 1.5f * hexMetrics.OuterRadius * hexCoordinate.X;
            localPosition.y = 0f;
            localPosition.z = hexMetrics.InnerRadius * (hexCoordinate.Z - hexCoordinate.Y);
        }
        else
        {
            localPosition.x = hexMetrics.InnerRadius * (hexCoordinate.X - hexCoordinate.Y);
            localPosition.y = 0f;
            localPosition.z = 1.5f * hexMetrics.OuterRadius * hexCoordinate.Z;
        }

        return localPosition * scale;
    }

    public static IHexCoordinate ToHexCoordinate(this Vector3 position, HexMetrics hexMetrics)
    {
        return ToHexCoordinate(position, hexMetrics, 1);
    }

    public static IHexCoordinate ToHexCoordinate(this Vector3 position, HexMetrics hexMetrics, float scale)
    {
        if (scale <= 0)
            return new HexCoordinate(0, 0);

        float fZ;
        float fY;
        float fX;

        if (hexMetrics.Orientation == HexOrientation.FlatUp)
        {
            fZ = position.z / (1.5f * hexMetrics.OuterRadius * scale);
            fY = -0.5f * (position.x / (hexMetrics.InnerRadius * scale) + fZ);
            fX = -fY - fZ;
        }
        else
        {
            fX = (position.x / scale) * 1.5f * hexMetrics.OuterRadius;
            fY = -0.5f * (position.x / (hexMetrics.InnerRadius * scale) + fX);
            fZ = -fX - fY;
        }

        int x = Mathf.RoundToInt(fX);
        int y = Mathf.RoundToInt(fY);
        int z = Mathf.RoundToInt(fZ);

        float dX = Mathf.Abs(fX - x);
        float dY = Mathf.Abs(fY - y);
        float dZ = Mathf.Abs(fZ - z);

        if (dX > dY && dX > dZ)
        {
            x = -y - z;
        }
        else if (dY > dZ)
        {
            y = -x - z;
        }

        return new HexCoordinate(x, y);
    }

    public static IEnumerable<ReadOnlyCollection<Vector3>> Triangulate(this IHexCoordinate hexCoordinate, HexMetrics hexMetrics)
    {
        return hexCoordinate.Triangulate(hexMetrics, 1);
    }

    public static IEnumerable<ReadOnlyCollection<Vector3>> Triangulate(this IHexCoordinate hexCoordinate, HexMetrics hexMetrics, float scale)
    {
        Vector3 center = hexCoordinate.ToLocalPosition(hexMetrics, scale);

        var hexTriangles = new int[][]
        {
            new int[] { 0, 2, 4 },
            new int[] { 0, 1, 2 },
            new int[] { 2, 3, 4 },
            new int[] { 4, 5, 0 },
        };

        foreach (int[] hexTriangle in hexTriangles)
        {
            yield return new ReadOnlyCollection<Vector3>(new[]
            {
                center + hexMetrics.Corners[hexTriangle[0]],
                center + hexMetrics.Corners[hexTriangle[1]],
                center + hexMetrics.Corners[hexTriangle[2]]
            });
        }
    }

    public static void MoveNE(this IHexCoordinate hexCoordinate, int distance)
    {
        hexCoordinate.Move(HexDirection.NE, distance);
    }

    public static void MoveE(this IHexCoordinate hexCoordinate, int distance)
    {
        hexCoordinate.Move(HexDirection.E, distance);
    }

    public static void MoveSE(this IHexCoordinate hexCoordinate, int distance)
    {
        hexCoordinate.Move(HexDirection.SE, distance);
    }

    public static void MoveSW(this IHexCoordinate hexCoordinate, int distance)
    {
        hexCoordinate.Move(HexDirection.SW, distance);
    }

    public static void MoveW(this IHexCoordinate hexCoordinate, int distance)
    {
        hexCoordinate.Move(HexDirection.W, distance);
    }

    public static void MoveNW(this IHexCoordinate hexCoordinate, int distance)
    {
        hexCoordinate.Move(HexDirection.NW, distance);
    }
}