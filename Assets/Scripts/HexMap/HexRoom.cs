using System;
using UnityEngine;
using UnityEngine.Events;

public class HexRoom : MonoBehaviour
{
    public delegate void ClickAction();

    [SerializeField]
    private bool showGizmos;

    [SerializeField]
    private Color defaultColor = Color.white;

    [SerializeField]
    private Color touchedColor = Color.magenta;

    [SerializeField]
    private int seed;
    public int Seed
    {
        get { return seed; }
        set { seed = value; }
    }

    [SerializeField]
    [Range(1, 10)]
    private int size = 6;
    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    [SerializeField]
    [Range(.1F, 2F)]
    private float scale = 1;
    public float Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    [SerializeField]
    private UnityEvent cellClicked;
    public UnityEvent CellClicked
    {
        get { return cellClicked; }
        set { cellClicked = value; }
    }

    public HexMetrics HexMetrics { get; private set; }

    public HexMap<HexTile> HexMap { get; private set; }

    private void Awake()
    {
        HexMetrics = new HexMetrics(1);
        HexMap = new HexMap<HexTile>(Size);

        foreach (IHexCoordinate hexCoordinate in HexMap.Coordinates)
        {
            HexMap[hexCoordinate] = new HexTile
            {
                Color = defaultColor,
            };
        }
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchCell(hit.point);
        }
    }

    private void TouchCell(Vector3 position)
    {
        Vector3 localPosition = transform.InverseTransformPoint(position);
        IHexCoordinate coordinate = localPosition.ToHexCoordinate(HexMetrics);
        HexMap[coordinate].Color = touchedColor;
        Debug.Log("touched at " + coordinate.ToString());

        if (CellClicked != null)
            CellClicked.Invoke();
    }
}