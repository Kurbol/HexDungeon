using System;
using UnityEngine;

[Serializable]
public struct HexCoordinate : IHexCoordinate
{
    [SerializeField]
    private int x;
    public int X { get { return x; } }

    [SerializeField]
    private int y;
    public int Y { get { return y; } }

    public int Z { get { return -X - Y; } }

    public HexCoordinate(IHexCoordinate hexCoordinate) : this(hexCoordinate.X, hexCoordinate.Y) { }

    public HexCoordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }
}