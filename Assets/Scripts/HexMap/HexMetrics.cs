using System;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public struct HexMetrics
{
    /* Orientation: ⬡ */

    public float HexWidth { get; private set; }
    public float InnerRadius { get; private set; }
    public float OuterRadius { get; private set; }
    public ReadOnlyCollection<Vector3> Corners { get; private set; }

    public HexMetrics(float hexWidth)
    {
        HexWidth = hexWidth;
        InnerRadius = HexWidth * 0.5f;
        OuterRadius = InnerRadius * 1.15470053838f; /* 2/sqrt(3) */

        Corners = new ReadOnlyCollection<Vector3>(new[]
        {
            new Vector3(0f, 0f, OuterRadius),
            new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
            new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3(0f, 0f, -OuterRadius),
            new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
        });
    }
}