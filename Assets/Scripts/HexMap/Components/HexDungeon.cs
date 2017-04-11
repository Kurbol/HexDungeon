using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexDungeon : MonoBehaviour, IHexGrid<IHexGrid<HexTile>>
{
    public delegate void ClickAction();

    [SerializeField]
    private bool showGizmos;

    [SerializeField]
    private HexRoom hexRoomPrefab;

    [SerializeField]
    [Range(1, 10)]
    private int size = 6;

    [SerializeField]
    [Range(.1F, 2F)]
    private float scale = 1;
    public float Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    [SerializeField]
    private HexOrientation hexOrientation;
    public HexOrientation HexOrientation
    {
        get { return hexOrientation; }
        set { hexOrientation = value; }
    }

    [SerializeField]
    private HexMetrics hexMetrics;
    public HexMetrics HexMetrics
    {
        get
        {
            if (hexMetrics.InnerRadius == 0)
            {
                float innerRadius = 1.5f * hexRoomPrefab.Size * hexRoomPrefab.HexMetrics.OuterRadius * hexRoomPrefab.Scale * Scale;
                hexMetrics = new HexMetrics(innerRadius, HexOrientation);
            }

            return hexMetrics;
        }
    }

    private Dictionary<IHexCoordinate, IHexGrid<HexTile>> hexMap;
    public Dictionary<IHexCoordinate, IHexGrid<HexTile>> HexMap
    {
        get
        {
            if (hexMap == null)
                hexMap = new Dictionary<IHexCoordinate, IHexGrid<HexTile>>();

            return hexMap;
        }
    }

    private void Awake()
    {
        StartCoroutine(BuildMapCells());
    }

    private IEnumerator BuildMapCells()
    {
        foreach (IHexCoordinate hexCoordinate in HexMapper.HexMap(size))
        {
            HexMap[hexCoordinate] = CreateHexRoom(hexCoordinate);

            yield return null;
        }
    }

    private HexRoom CreateHexRoom(IHexCoordinate hexCoordinate)
    {
        Vector3 localPosition = hexCoordinate.ToLocalPosition(HexMetrics);

        HexRoom hexRoom = Instantiate(hexRoomPrefab);
        hexRoom.transform.SetParent(transform, false);
        hexRoom.transform.localPosition = localPosition;

        return hexRoom;
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos)
        {
            return;
        }
    }
}