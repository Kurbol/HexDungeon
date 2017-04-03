using UnityEngine;

public class HexDungeon : MonoBehaviour
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
    private HexOrientation hexOrientation = HexOrientation.PointUp;
    public HexOrientation HexOrientation
    {
        get { return hexOrientation; }
        set { hexOrientation = value; }
    }

    private HexMetrics hexMetrics;
    public HexMetrics HexMetrics
    {
        get
        {
            if (hexMetrics.InnerRadius <= 0)
            {
                float innerRadius = 1.5f * hexRoomPrefab.Size * hexRoomPrefab.HexMetrics.OuterRadius * Scale;
                hexMetrics = new HexMetrics(innerRadius, HexOrientation);
            }

            return hexMetrics;
        }
    }

    private HexMap<HexRoom> hexMap;
    public HexMap<HexRoom> HexMap
    {
        get
        {
            hexMap = hexMap ?? new HexMap<HexRoom>(Size);
            return hexMap;
        }
    }

    private void Awake()
    {
        foreach (IHexCoordinate hexCoordinate in HexMap.Coordinates)
        {
            HexMap[hexCoordinate] = CreateHexRoom(hexCoordinate);
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