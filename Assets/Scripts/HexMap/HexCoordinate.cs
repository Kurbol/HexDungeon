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

    public void Move(HexDirection direction, int distance)
    {
        switch (direction)
        {
            case HexDirection.NE:
                Y -= distance;
                break;

            case HexDirection.E:
                X += distance;
                Y -= distance;
                break;

            case HexDirection.SE:
                X += distance;
                break;

            case HexDirection.SW:
                Y += distance;
                break;

            case HexDirection.W:
                X -= distance;
                Y += distance;
                break;

            case HexDirection.NW:
                X -= distance;
                break;
        }
    }
}