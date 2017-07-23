using System;
using System.Collections.Generic;
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
        {
            IDictionary<IHexCoordinate, IHexTile> neighbors = hexGrid.HexTiles.Neighbors(hexTile.HexCoordinate);
            var triangulateSections = new List<IEnumerable<Triangle>>
            {
                hexTile.HexCoordinate.TriangulateInner(hexTile.HexMetrics, hexTile.Color),
                neighbors.TriangulateBridge(hexTile, HexDirection.One),
                neighbors.TriangulateBridge(hexTile, HexDirection.Two),
                neighbors.TriangulateBridge(hexTile, HexDirection.Three),
            };

            foreach (IEnumerable<Triangle> triangulateSection in triangulateSections)
                foreach (Triangle triangle in triangulateSection)
                    meshBuilder.AddTriangle(triangle);
        }

        return meshBuilder.ToMesh();
    }
}