using Boo.Lang;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public static class HexMetricsExtensions
{
    public static ReadOnlyCollection<Vector3> InnerCorners(this HexMetrics hexMetrics)
    {
        return new ReadOnlyCollection<Vector3>(hexMetrics.Corners.Select(a => a * hexMetrics.SolidFactor).ToList());
    }

    public static ReadOnlyCollection<Vector3> InnerCorners(this HexMetrics hexMetrics, HexDirection direction)
    {
        ReadOnlyCollection<Vector3> innerCorners = hexMetrics.InnerCorners();

        return new ReadOnlyCollection<Vector3>(new List<Vector3>
        {
            innerCorners[(int)direction],
            innerCorners[(int)(direction.Next())],
        });
    }
}