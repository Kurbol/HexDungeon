using System;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public struct HexMetrics
{
    public HexOrientation Orientation { get; private set; }

    public float InnerRadius { get; private set; }
    public float OuterRadius { get; private set; }
    public ReadOnlyCollection<Vector3> Corners { get; private set; }

    public HexMetrics(float innerRadius, HexOrientation orientation)
    {
        Orientation = orientation;
        InnerRadius = innerRadius;
        OuterRadius = InnerRadius * 1.15470053838f; /* 2/sqrt(3) */

        if (Orientation == HexOrientation.FlatUp)
        {
            Corners = new ReadOnlyCollection<Vector3>(new[]
            {
                new Vector3(0.5f * OuterRadius, 0f, InnerRadius),
                new Vector3(OuterRadius, 0f, 0f),
                new Vector3(0.5f * OuterRadius, 0f, -InnerRadius),
                new Vector3(-0.5f * OuterRadius, 0f, -InnerRadius),
                new Vector3(-OuterRadius, 0f, -0f),
                new Vector3(-0.5f * OuterRadius, 0f, InnerRadius),
            });
        }
        else
        {
            Corners = new ReadOnlyCollection<Vector3>(new[]
            {
                new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
                new Vector3(0f, 0f, -OuterRadius),
                new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
                new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
                new Vector3(0f, 0f, OuterRadius),
                new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
            });
        }
    }
}