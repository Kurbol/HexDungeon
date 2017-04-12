using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
    private HexMetrics hexMetrics;
    public HexMetrics HexMetrics
    {
        get
        {
            if (hexMetrics.InnerRadius == 0)
            {
                float innerRadius = hexRoomPrefab.InnerRadius() * Scale;
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
            {
                hexMap = new Dictionary<IHexCoordinate, IHexGrid<HexTile>>();
            }

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