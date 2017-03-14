using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(MeshCollider), typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    [SerializeField]
    private HexRoom hexRoom;

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshCollider meshCollider;

    private void Start()
    {
        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }

        if (meshCollider == null)
        {
            meshCollider = GetComponent<MeshCollider>();
        }

        RebuildMesh();
    }

    public void RebuildMesh()
    {
        Mesh mesh = BuildMesh(hexRoom);

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    private static Mesh BuildMesh(HexRoom hexRoom)
    {
        MeshBuilder meshBuilder = new MeshBuilder("Hex Mesh");

        foreach (HexCell<HexTile> hexCell in hexRoom.HexMap)
        {
            foreach (ReadOnlyCollection<Vector3> triangle in hexCell.Coordinate.Triangulate(hexRoom.HexMetrics))
            {
                if (hexCell.Tile != null)
                    meshBuilder.AddTriangle(triangle[0], triangle[1], triangle[2], hexCell.Tile.Color);
            }
        }

        return meshBuilder.ToMesh();
    }
}