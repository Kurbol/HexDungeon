using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(MeshCollider), typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    [SerializeField]
    private HexRoom hexRoom;

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
        if (MeshFilter.mesh == null || MeshCollider.sharedMesh == null)
            RebuildMesh();
    }

    public void RebuildMesh()
    {
        Mesh mesh = BuildMesh(hexRoom);

        MeshFilter.mesh = mesh;
        MeshCollider.sharedMesh = mesh;
    }

    private static Mesh BuildMesh(HexRoom hexRoom)
    {
        MeshBuilder meshBuilder = new MeshBuilder("Hex Mesh");

        foreach (KeyValuePair<IHexCoordinate, HexTile> hexCell in hexRoom.HexMap)
        {
            foreach (ReadOnlyCollection<Vector3> triangle in hexCell.Key.Triangulate(hexRoom.HexMetrics))
            {
                if (hexCell.Value != null)
                    meshBuilder.AddTriangle(triangle[0], triangle[1], triangle[2], hexCell.Value.Color);
            }
        }

        return meshBuilder.ToMesh();
    }
}