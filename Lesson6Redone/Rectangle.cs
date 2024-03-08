// ---- C# I (Dor Ben Dor) ----
//        Daniel Fitzer
// ----------------------------


//Exercise B

namespace myNamespace
{
    public class Rectangle
    {
        public int Width;
        public int Height;
        public Point RectangleCenter;

        public int Volume => Width * Height;
        public int Circumference => Width * 2 + Height * 2;

        public bool IsPointInRectangle(Point point, Rectangle rectangle)
        {
            if (point.X > rectangle.RectangleCenter.X - Width / 2 && point.X < rectangle.RectangleCenter.X + Width / 2 && point.Y > rectangle.RectangleCenter.Y - Height / 2 && point.Y < rectangle.RectangleCenter.Y + Height / 2)
            { return true; }
            else
            {
                return false;
            }
        }
        public bool DoRectanglesIntersect(Rectangle rectangle, Rectangle rectangle2)//I believe there is a way to make this shorter and more efficient,
        {                                                                           //either by making one function that goes twice, once for r1 in r2,
            for(int i = rectangle.RectangleCenter.X - rectangle.Width / 2; i < rectangle.RectangleCenter.X + rectangle.Width / 2; i++)//and once for r2 in r1.
            {                                                                                                                         //or some other way, but i got no time to figure it out and this works.
                Point point = new Point(i, rectangle.RectangleCenter.Y + rectangle.Height / 2);
                if (IsPointInRectangle(point, rectangle2))
                {
                    return true;
                }
            }
            for (int i = rectangle.RectangleCenter.X - rectangle.Width / 2; i < rectangle.RectangleCenter.X + rectangle.Width / 2; i++)
            {
                Point point = new Point(i, rectangle.RectangleCenter.Y - rectangle.Height / 2);
                if (IsPointInRectangle(point, rectangle2))
                {
                    return true;
                }
            }
            for (int i = rectangle.RectangleCenter.Y - rectangle.Height / 2; i < rectangle.RectangleCenter.Y + rectangle.Height / 2; i++)
            {
                Point point = new Point(rectangle.RectangleCenter.X + rectangle.Width / 2, i);
                if (IsPointInRectangle(point, rectangle2))
                {
                    return true;
                }
            }
            for (int i = rectangle.RectangleCenter.Y - rectangle.Height / 2; i < rectangle.RectangleCenter.Y + rectangle.Height / 2; i++)
            {
                Point point = new Point(rectangle.RectangleCenter.X - rectangle.Width / 2, i);
                if (IsPointInRectangle(point, rectangle2))
                {
                    return true;
                }
            }
            for (int i = rectangle2.RectangleCenter.X - rectangle2.Width / 2; i < rectangle2.RectangleCenter.X + rectangle2.Width / 2; i++)
            {
                Point point = new Point(i, rectangle2.RectangleCenter.Y + rectangle2.Height / 2);
                if (IsPointInRectangle(point, rectangle))
                {
                    return true;
                }
            }
            for (int i = rectangle2.RectangleCenter.X - rectangle2.Width / 2; i < rectangle2.RectangleCenter.X + rectangle2.Width / 2; i++)
            {
                Point point = new Point(i, rectangle2.RectangleCenter.Y - rectangle2.Height / 2);
                if (IsPointInRectangle(point, rectangle))
                {
                    return true;
                }
            }
            for (int i = rectangle2.RectangleCenter.Y - rectangle2.Height / 2; i < rectangle2.RectangleCenter.Y + rectangle2.Height / 2; i++)
            {
                Point point = new Point(rectangle2.RectangleCenter.X + rectangle2.Width / 2, i);
                if (IsPointInRectangle(point, rectangle))
                {
                    return true;
                }
            }
            for (int i = rectangle2.RectangleCenter.Y - rectangle2.Height / 2; i < rectangle2.RectangleCenter.Y + rectangle2.Height / 2; i++)
            {
                Point point = new Point(rectangle2.RectangleCenter.X - rectangle2.Width / 2, i);
                if (IsPointInRectangle(point, rectangle))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}