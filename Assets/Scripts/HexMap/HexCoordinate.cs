using System;
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
}