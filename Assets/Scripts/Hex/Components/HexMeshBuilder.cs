using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(MeshCollider), typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMeshBuilder : MonoBehaviour
{
    [SerializeField]
    private HexGrid hexGrid;
    public HexGrid HexGrid { get { return hexGrid; } set { hexGrid = value; } }

    [SerializeField]
    private MeshFilter meshFilter;
    public MeshFilter MeshFilter
    {
        get
        {
            meshFilter = meshFilter ?? GetComponent<MeshFilter>();
            return meshFilter;
        }
    }

    [SerializeField]
    private MeshCollider meshCollider;
    public MeshCollider MeshCollider
    {
        get
        {
            meshCollider = meshCollider ?? GetComponent<MeshCollider>();
            return meshCollider;
        }
    }

    private void Start()
    {
        if (MeshFilter.sharedMesh == null || MeshCollider.sharedMesh == null)
            RebuildMesh();
    }

    public void RebuildMesh()
    {
        Mesh mesh = BuildMesh(hexGrid);

        DestroyImmediate(MeshFilter.sharedMesh);
        DestroyImmediate(MeshCollider.sharedMesh);

        MeshFilter.sharedMesh = mesh;
        MeshCollider.sharedMesh = mesh;
    }

    private static Mesh BuildMesh(HexGrid hexGrid)
    {
        MeshBuilder meshBuilder = new MeshBuilder("Hex Mesh");

        foreach (HexTile hexTile in hexGrid.HexTiles.Values)
            foreach (Triangle triangle in hexTile.HexCoordinate.Triangulate(hexTile.HexMetrics))
            {
                // ToDo get neighbors to figure our their colors
                // Blend colors in AddTriangle()
                // Separate Color logic from triangulation? I guess it's still part of building the mesh though.

                meshBuilder.AddTriangle(triangle, hexTile.Color);
            }

        return meshBuilder.ToMesh();
    }
}