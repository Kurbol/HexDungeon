using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class HexMap<T> : IEnumerable<HexCell<T>>
{
    public int Size { get; private set; }

    private Dictionary<IHexCoordinate, T> HexCells { get; set; }

    public IHexCoordinate[] Coordinates { get { return HexCells.Keys.ToArray(); } }

    public int CellCount { get { return HexCells.Count; /* 6 * (Size * (Size - 1) / 2) + 1 */ } }

    public T this[IHexCoordinate coordinate]
    {
        get
        {
            if (HexCells.ContainsKey(coordinate))
            {
                return HexCells[coordinate];
            }
            else
            {
                throw new InvalidOperationException("Coordinate does not exist: " + coordinate.ToString());
            }
        }

        set
        {
            if (HexCells.ContainsKey(coordinate))
            {
                HexCells[coordinate] = value;
            }
            else
            {
                throw new InvalidOperationException("Coordinate does not exist: " + coordinate.ToString());
            }
        }
    }

    public HexMap(int size)
    {
        Size = size;
        HexCells = CreateHexCells(Size);
    }

    public IEnumerator<HexCell<T>> GetEnumerator()
    {
        foreach (KeyValuePair<IHexCoordinate, T> hexCell in HexCells)
            yield return new HexCell<T>(hexCell);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool ContainsCoordinate(IHexCoordinate coordinate)
    {
        return HexCells.ContainsKey(coordinate);
    }

    public bool ContainsCell(T cell)
    {
        return HexCells.ContainsValue(cell);
    }

    public bool TryGetCell(IHexCoordinate coordinate, out T cell)
    {
        cell = default(T);

        if (HexCells.ContainsKey(coordinate))
        {
            cell = HexCells[coordinate];
            return true;
        }

        return false;
    }

    private static Dictionary<IHexCoordinate, T> CreateHexCells(int hexMapSize)
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