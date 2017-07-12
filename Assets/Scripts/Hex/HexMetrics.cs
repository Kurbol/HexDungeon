using System;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public struct HexMetrics
{
    [SerializeField]
    private HexOrientation orientation;
    public HexOrientation Orientation { get { return orientation; } }

    [SerializeField]
    private float innerRadius;
    public float InnerRadius { get { return innerRadius; } }

    public float OuterRadius { get { return InnerRadius * 1.15470053838f; /* 2/sqrt(3) */ } }

    public ReadOnlyCollection<Vector3> Corners
    {
        get
        {
            if (Orientation == HexOrientation.PointUp)
            {
                return new ReadOnlyCollection<Vector3>(new[]
                {
                    new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
                    new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
                    new Vector3(0f, 0f, -OuterRadius),
                    new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
                    new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
                    new Vector3(0f, 0f, OuterRadius),
                });
            }
            else
            {
                return new ReadOnlyCollection<Vector3>(new[]
                {
                    new Vector3(OuterRadius, 0f, 0f),
                    new Vector3(0.5f * OuterRadius, 0f, -InnerRadius),
                    new Vector3(-0.5f * OuterRadius, 0f, -InnerRadius),
                    new Vector3(-OuterRadius, 0f, 0f),
                    new Vector3(-0.5f * OuterRadius, 0f, InnerRadius),
                    new Vector3(0.5f * OuterRadius, 0f, InnerRadius),
                });
            }
        }
    }

    public HexMetrics(float innerRadius, HexOrientation orientation)
    {
        this.orientation = orientation;
        this.innerRadius = innerRadius;
    }
}