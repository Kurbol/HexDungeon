using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshBuilder
{
    private readonly Mesh mesh;
    private List<Vector3> Vertices { get; set; }
    private List<int> Triangles { get; set; }
    private List<Color> Colors { get; set; }

    public MeshBuilder(string name)
    {
        mesh = new Mesh() { name = name };
        Vertices = new List<Vector3>();
        Triangles = new List<int>();
        Colors = new List<Color>();
    }

    public void AddTriangle(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
    {
        int vertexIndex = Vertices.Count;

        Vertices.Add(vertex1);
        Vertices.Add(vertex2);
        Vertices.Add(vertex3);

        Triangles.Add(vertexIndex);
        Triangles.Add(vertexIndex + 1);
        Triangles.Add(vertexIndex + 2);
    }

    public void AddTriangle(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, Color color)
    {
        AddTriangle(vertex1, vertex2, vertex3);

        Colors.Add(color);
        Colors.Add(color);
        Colors.Add(color);
    }

    public Mesh ToMesh()
    {
        mesh.vertices = Vertices.ToArray();
        mesh.triangles = Triangles.ToArray();

        if (Colors.Any())
            mesh.colors = Colors.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}