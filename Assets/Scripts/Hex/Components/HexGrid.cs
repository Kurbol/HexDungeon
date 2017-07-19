using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class HexGrid : MonoBehaviour, IHexGrid
{
    private const string TileHolderName = "Tiles";

    [SerializeField]
    private bool showGizmos;

    [SerializeField]
    private HexTile hexTilePrefab;

    [SerializeField]
    [Range(1, 10)]
    private int size = 6;
    public int Size
    {
        get { return size; }
        private set { size = value; }
    }

    [SerializeField]
    private Dictionary<IHexCoordinate, IHexTile> hexTiles = new Dictionary<IHexCoordinate, IHexTile>();
    public Dictionary<IHexCoordinate, IHexTile> HexTiles { get { return hexTiles; } }

    private void Awake()
    {
        var tileHolder = transform.GetChild(TileHolderName);
        tileHolder.localPosition = Vector3.zero;

        HexTiles.Clear();
        HexTile[] hexTiles = tileHolder.GetComponentsInChildren<HexTile>();
        if (hexTiles.Any())
        {
            foreach (HexTile HexTile in hexTiles)
                HexTiles[HexTile.HexCoordinate] = HexTile;
        }
        else
        {
            HexTiles.Populate(Size, (IHexCoordinate hexCoordinate) => CreateHexTile(hexCoordinate, hexTilePrefab, tileHolder));
        }
    }

    private static HexTile CreateHexTile(IHexCoordinate hexCoordinate, HexTile hexTilePrefab, Transform parent)
    {
        HexTile hexTile = Instantiate(hexTilePrefab);
        hexTile.transform.SetParent(parent, false);
        hexTile.transform.localPosition = hexCoordinate.ToLocalPosition(hexTile.HexMetrics);
        hexTile.HexCoordinate = new HexCoordinate(hexCoordinate);

        return hexTile;
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;
    }
}