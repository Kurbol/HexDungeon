using System.Collections.ObjectModel;
using UnityEngine;

public class Triangle : ReadOnlyCollection<Vector3>
{
    public Vector3 Corner1 { get { return base[0]; } }
    public Vector3 Corner2 { get { return base[1]; } }
    public Vector3 Corner3 { get { return base[2]; } }

    public Triangle(Vector3 corner1, Vector3 corner2, Vector3 corner3) : base(new[] { corner1, corner2, corner3 })
    {
    }
}
