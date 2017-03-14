using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public struct HexCoordinate : IHexCoordinate
{
    public int X { get; private set; }

    public int Y { get; private set; }

    public int Z { get { return -X - Y; } }

    public HexCoordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    public static IHexCoordinate FromLocalPosition(Vector3 position, HexMetrics hexMetrics)
    {
        return FromLocalPosition(position, hexMetrics, 1);
    }

    public static IHexCoordinate FromLocalPosition(Vector3 position, HexMetrics hexMetrics, float scale)
    {
        if (scale <= 0)
            return new HexCoordinate(0, 0);

        float fZ = position.z / (1.5f * hexMetrics.OuterRadius * scale);
        float fY = -0.5f * (position.x / (hexMetrics.InnerRadius * scale) + fZ);
        float fX = -fY - fZ;

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

    public void AddX(int value)
    {
        X += value;
        Y -= value;
    }

    public void AddY(int value)
    {
        Y -= value;
    }

    public void AddZ(int value)
    {
        X -= value;
    }
}

public static class HexCoordinateExtensions
{
    public static Vector3 ToLocalPosition(this IHexCoordinate hexCoordinate, HexMetrics hexMetrics)
    {
        return hexCoordinate.ToLocalPosition(hexMetrics, 1);
    }

    public static Vector3 ToLocalPosition(this IHexCoordinate hexCoordinate, HexMetrics hexMetrics, float scale)
    {
        if (scale <= 0)
            return new Vector3();

        float worldX = hexMetrics.InnerRadius * (hexCoordinate.X - hexCoordinate.Y);
        float worldY = 0f;
        float worldZ = 1.5f * hexMetrics.OuterRadius * hexCoordinate.Z;

        return new Vector3(worldX, worldY, worldZ) * scale;
    }

    public static IHexCoordinate ToHexCoordinate(this Vector3 position, HexMetrics hexMetrics)
    {
        return ToHexCoordinate(position, hexMetrics, 1);
    }

    public static IHexCoordinate ToHexCoordinate(this Vector3 position, HexMetrics hexMetrics, float scale)
    {
        if (scale <= 0)
            return new HexCoordinate(0, 0);

        float fZ = position.z / (1.5f * hexMetrics.OuterRadius * scale);
        float fY = -0.5f * (position.x / (hexMetrics.InnerRadius * scale) + fZ);
        float fX = -fY - fZ;

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
}