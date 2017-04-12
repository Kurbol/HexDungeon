public static class HexGridExtensions
{
    public static float InnerRadius<T>(this IHexGrid<T> hexGrid)
    {
        return 1.5f * hexGrid.Size * hexGrid.HexMetrics.OuterRadius * hexGrid.Scale;
    }
}