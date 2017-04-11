using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class HexMapper
{
    public static IEnumerable<IHexCoordinate> HexMap(int size)
    {
        for (int i = 1; i <= size; i++)
        {
            foreach (IHexCoordinate coordinate in HexRing(i))
            {
                yield return coordinate;
            }
        }
    }

    public static IEnumerable<IHexCoordinate> HexRing(int size)
    {
        if (size <= 0)
        {
            yield break;
        }

        if (size == 1)
        {
            yield return new HexCoordinate(0, 0);
            yield break;
        }

        for (int x = 1, y = -(size - 1); x < size; x++)
        {
            yield return new HexCoordinate(x, y);
            yield return new HexCoordinate(-x, -y);
        }

        for (int x = size - 1, y = -(size - 2); y <= 0; y++)
        {
            yield return new HexCoordinate(x, y);
            yield return new HexCoordinate(-x, -y);
        }

        for (int x = size - 2, y = 1; y < size && x >= 0; x--, y++)
        {
            yield return new HexCoordinate(x, y);
            yield return new HexCoordinate(-x, -y);
        }
    }

    public static int HexMapCellCount(int size)
    {
        return 6 * (size * (size - 1) / 2) + 1;
    }

    public static int HexRingCellCount(int size)
    {
        return 6 * (size - 1);
    }

    public static int MapSize(this IEnumerable<IHexCoordinate> hexMap)
    {
        if (hexMap == null || !hexMap.Any())
            return 0;

        int maxSize = 1;
        foreach (IHexCoordinate c in hexMap)
        {
            int size = Mathf.Max(Mathf.Abs(c.X), Mathf.Abs(c.Y), Mathf.Abs(c.Z)) + 1;
            if (size > maxSize)
                maxSize = size;
        }

        return maxSize;
    }

    [Obsolete]
    private static Dictionary<IHexCoordinate, T> CreateHexCells<T>(int hexMapSize)
    {
        var hexMap = new Dictionary<IHexCoordinate, T>();
        var hexCoordinate = new HexCoordinate(0, 0);
        var defaultValue = default(T);

        hexMap[hexCoordinate] = defaultValue;
        for (int i = 1, x = 0; x < hexMapSize; x++)
        {
            for (int y = -hexMapSize + 1; x + y < hexMapSize; y++, i += 2)
            {
                if (x == 0 && y >= 0)
                    break;

                hexCoordinate = new HexCoordinate(x, y);
                hexMap[hexCoordinate] = defaultValue;

                hexCoordinate = new HexCoordinate(-x, -y);
                hexMap[hexCoordinate] = defaultValue;
            }
        }

        return hexMap;
    }
}