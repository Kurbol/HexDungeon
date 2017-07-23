public static class HexDirectionExtensions
{
    public static HexDirection Previous(this HexDirection direction)
    {
        return direction.Next(5);
    }

    public static HexDirection Next(this HexDirection direction)
    {
        return direction.Next(1);
    }

    public static HexDirection Next(this HexDirection direction, int increment)
    {
        var a = ((int)direction + increment) % 6;
        var b = (HexDirection)a;

        return (HexDirection)(((int)direction + increment) % 6);
    }
}
