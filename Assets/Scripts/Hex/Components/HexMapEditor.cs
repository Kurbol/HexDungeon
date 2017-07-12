using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour
{
    [SerializeField]
    private Color[] colors;
    public Color[] Colors { get { return colors; } set { colors = value; } }

    private Color activeColor;

    private void Awake()
    {
        SelectColor(0);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
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
            HexGrid hexGrid = hit.transform.GetComponent<HexGrid>();
            if (hexGrid == null)
                return;

            IHexTile hexTile = hexGrid.GetHexTileFromWorldPosition(hit.point);
            if (hexTile == null)
                return;

            hexTile.Color = activeColor;
            Debug.Log("touched at " + hexTile.HexCoordinate.ToString());

            // ToDo Somehow signal HexGrid to rebuild HexMesh
            if (CellClicked != null)
                CellClicked.Invoke();
        }
    }

    public void SelectColor(int index)
    {
        activeColor = colors[index];
    }
}