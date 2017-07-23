using System.Collections.ObjectModel;
using UnityEngine;

public class Triangle : ReadOnlyCollection<Vector3>
{
    public Vector3 Corner1 { get { return base[0]; } }
    public Vector3 Corner2 { get { return base[1]; } }
    public Vector3 Corner3 { get { return base[2]; } }

    public Color Color1 { get; set; }
    public Color Color2 { get; set; }
    public Color Color3 { get; set; }

    public Triangle(Vector3 corner1, Vector3 corner2, Vector3 corner3) : base(new[] { corner1, corner2, corner3 })
    {
    }

    public Triangle(Vector3 corner1, Vector3 corner2, Vector3 corner3, Color color1, Color color2, Color color3) : this(corner1, corner2, corner3)
    {
        Color1 = color1;
        Color2 = color2;
        Color3 = color3;
    }
}
