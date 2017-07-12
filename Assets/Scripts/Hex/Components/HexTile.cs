using System;
using UnityEngine;

[Serializable]
public class HexTile : MonoBehaviour, IHexTile
{
    [SerializeField]
    private HexCoordinate hexCoordinate;
    public HexCoordinate HexCoordinate
    {
        get { return hexCoordinate; }
        set { hexCoordinate = value; }
    }

    [SerializeField]
    private HexMetrics hexMetrics;
    public HexMetrics HexMetrics
    {
        get
        {
            return hexMetrics;
        }
    }

    [SerializeField]
    private Color color;
    public Color Color
    {
        get { return color; }
        set { color = value; }
    }
}