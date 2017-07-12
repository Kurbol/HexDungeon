using System;
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

        if (hexMetrics.Orientation == HexOrientation.PointUp)
        {
            localPosition.x = hexMetrics.InnerRadius * (hexCoordinate.X - hexCoordinate.Y);
            localPosition.y = 0f;
            localPosition.z = 1.5f * hexMetrics.OuterRadius * hexCoordinate.Z;
        }
        else
        {
            localPosition.x = 1.5f * hexMetrics.OuterRadius * hexCoordinate.X;
            localPosition.y = 0f;
            localPosition.z = hexMetrics.InnerRadius * (hexCoordinate.Z - hexCoordinate.Y);
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

        if (hexMetrics.Orientation == HexOrientation.PointUp)
        {
            fZ = position.z / (1.5f * hexMetrics.OuterRadius * scale);
            fY = -0.5f * (position.x / (hexMetrics.InnerRadius * scale) + fZ);
            fX = -fY - fZ;
        }
        else
        {
            fX = position.x / (1.5f * hexMetrics.OuterRadius * scale);
            fY = -0.5f * (position.z / (hexMetrics.InnerRadius * scale) + fX);
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

    public static IHexCoordinate Neighbor(this IHexCoordinate hexCoordinate, HexDirection direction)
    {
        return hexCoordinate.Neighbor(direction, 1);
    }

    public static IHexCoordinate Neighbor(this IHexCoordinate hexCoordinate, HexDirection direction, int distance)
    {
        //       _One_
        //   Six/     \Two
        //     /       \
        // Five\       /Three
        //      \_____/
        //       Four

        //    Six / \ One
        //      /     \
        // Five|       |Two
        //     |       |
        // Four \     / Three
        //        \ /

        int x = hexCoordinate.X;
        int y = hexCoordinate.Y;

        switch (direction)
        {
            case HexDirection.One:
                y -= distance;
                break;

            case HexDirection.Two:
                x += distance;
                y -= distance;
                break;

            case HexDirection.Three:
                x += distance;
                break;

            case HexDirection.Four:
                y += distance;
                break;

            case HexDirection.Five:
                x -= distance;
                y += distance;
                break;

            case HexDirection.Six:
                x -= distance;
                break;
        }

        return new HexCoordinate(x, y);
    }

    public static IEnumerable<IHexCoordinate> Neighbors(this IHexCoordinate hexCoordinate)
    {
        foreach (HexDirection hexDirection in Enum.GetValues(typeof(HexDirection)))
        {
            yield return hexCoordinate.Neighbor(hexDirection);
        }
    }
}