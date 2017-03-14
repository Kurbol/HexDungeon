using UnityEngine;
using UnityEngine.UI;

public class HexMapLabels : MonoBehaviour
{
    [SerializeField]
    private Text cellLabelPrefab;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private HexRoom hexRoom;

    private void Start()
    {
        foreach (IHexCoordinate hexCoordinate in hexRoom.HexMap.Coordinates)
        {
            CreateHexLabel(hexCoordinate);
        }
    }

    private void CreateHexLabel(IHexCoordinate hexCoordinate)
    {
        Vector3 localPosition = hexCoordinate.ToLocalPosition(hexRoom.HexMetrics);

        Text label = Instantiate(cellLabelPrefab);
        label.rectTransform.SetParent(canvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(localPosition.x, localPosition.z);
        label.text = hexCoordinate.ToString();
    }
}