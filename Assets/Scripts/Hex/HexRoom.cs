using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HexRoom : MonoBehaviour, IHexGrid<HexTile>
{
    public delegate void ClickAction();

    [SerializeField]
    private bool showGizmos;

    [SerializeField]
    private Color defaultColor = Color.white;

    [SerializeField]
    [Range(1, 10)]
    private int size = 6;
    public int Size
    {
        get { return size; }
        private set { size = value; }
    }

    [SerializeField]
    [Range(.1F, 2F)]
    private float scale = 1;
    public float Scale
    {
        get { return scale; }
        private set { scale = value; }
    }

    [SerializeField]
    private HexOrientation hexOrientation;
    public HexOrientation HexOrientation
    {
        get { return hexOrientation; }
        private set { hexOrientation = value; }
    }

    [SerializeField]
    private UnityEvent cellClicked;
    public UnityEvent CellClicked
    {
        get { return cellClicked; }
        set { cellClicked = value; }
    }

    [SerializeField]
    private HexMetrics hexMetrics;
    public HexMetrics HexMetrics
    {
        get
        {
            if (hexMetrics.InnerRadius <= 0)
            {
                float innerRadius = 0.5f * Scale;
                hexMetrics = new HexMetrics(innerRadius, HexOrientation);
            }

            return hexMetrics;
        }
    }

    [SerializeField]
    private Dictionary<IHexCoordinate, HexTile> hexMap;
    public Dictionary<IHexCoordinate, HexTile> HexMap
    {
        get
        {
            if (hexMap == null)
            {
                hexMap = new Dictionary<IHexCoordinate, HexTile>();
                hexMap.Populate(Size, () => new HexTile { Color = defaultColor });
            }

            return hexMap;
        }
    }

    public void ColorCell(Vector3 worldPosition, Color color)
    {
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
        IHexCoordinate coordinate = localPosition.ToHexCoordinate(HexMetrics);

        if (!HexMap.ContainsKey(coordinate))
            return;

        HexMap[coordinate].Color = color;
        Debug.Log("touched at " + coordinate.ToString());

        if (CellClicked != null)
        {
            CellClicked.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos)
        {
            return;
        }
    }
}