using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HexMapLabels : MonoBehaviour
{
    [SerializeField]
    private Text cellLabelPrefab;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private HexGrid hexGrid;

    private void Start()
    {
        foreach (HexTile hexTile in hexGrid.HexTiles.Values)
        {
            CreateHexLabel(hexTile);
        }
    }

    private void CreateHexLabel(HexTile hexTile)
    {
        Vector3 localPosition = hexTile.HexCoordinate.ToLocalPosition(hexTile.HexMetrics);

        Text label = Instantiate(cellLabelPrefab);
        label.rectTransform.SetParent(canvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(localPosition.x, localPosition.z);
        label.text = hexTile.HexCoordinate.ToString();
    }
}