using System;
using System.Collections.Generic;

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

    public static void Populate<T>(this Dictionary<IHexCoordinate, T> hexMap, int size, Func<T> tileGenerator)
    {
        foreach (IHexCoordinate hexCoordinate in HexMap(size))
        {
            hexMap[hexCoordinate] = tileGenerator();
        }
    }

    public static void Populate<T>(this Dictionary<IHexCoordinate, T> hexMap, int size, Func<IHexCoordinate, T> tileGenerator)
    {
        foreach (IHexCoordinate hexCoordinate in HexMap(size))
        {
            hexMap[hexCoordinate] = tileGenerator(hexCoordinate);
        }
    }
}