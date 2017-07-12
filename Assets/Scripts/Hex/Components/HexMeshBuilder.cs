using System;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(MeshCollider), typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMeshBuilder : MonoBehaviour
{
    [SerializeField]
    private HexRoom hexRoom;
    public HexRoom HexRoom { get { return hexRoom; } set { hexRoom = value; } }

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
        Mesh mesh = BuildMesh(hexRoom);

        DestroyImmediate(MeshFilter.sharedMesh);
        DestroyImmediate(MeshCollider.sharedMesh);

        MeshFilter.sharedMesh = mesh;
        MeshCollider.sharedMesh = mesh;
    }

    private static Mesh BuildMesh(HexRoom hexRoom)
    {
        MeshBuilder meshBuilder = new MeshBuilder("Hex Mesh");

        foreach (HexTile hexTile in hexRoom.HexTiles.Values)
            foreach (ReadOnlyCollection<Vector3> triangle in hexTile.HexCoordinate.Triangulate(hexTile.HexMetrics))
                meshBuilder.AddTriangle(triangle[0], triangle[1], triangle[2], hexTile.Color);

        return meshBuilder.ToMesh();
    }
}