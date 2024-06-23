public class Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
    public override string ToString()
    {
        return $"X: {X}, Y: {Y}";
    }
    public Point(Point point)
    {
        this.X = point.X;
        this.Y = point.Y;
    }
}