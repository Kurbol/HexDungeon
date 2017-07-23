using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshBuilder
{
    private readonly Color defaultColor;
    private readonly Mesh mesh;
    private List<Vector3> Vertices { get; set; }
    private List<int> Triangles { get; set; }
    private List<Color> Colors { get; set; }

    public MeshBuilder(string name)
    {
        defaultColor = Color.white;
        mesh = new Mesh() { name = name };
        Vertices = new List<Vector3>();
        Triangles = new List<int>();
        Colors = new List<Color>();
    }

    public MeshBuilder(string name, Color defaultColor) : this(name)
    {
        this.defaultColor = defaultColor;
    }

    public void AddTriangle(Triangle triangle)
    {
        int vertexIndex = Vertices.Count;

        Vertices.Add(triangle.Corner1);
        Vertices.Add(triangle.Corner2);
        Vertices.Add(triangle.Corner3);

        Triangles.Add(vertexIndex);
        Triangles.Add(vertexIndex + 1);
        Triangles.Add(vertexIndex + 2);

        Colors.Add(triangle.Color1);
        Colors.Add(triangle.Color2);
        Colors.Add(triangle.Color3);
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