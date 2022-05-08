using System.Collections;
using System.Collections.ObjectModel;
using static System.Console;

var vectorObjects = new List<VectorObject>
{
    new VectorRectangle(1, 1, 10, 10),
    new VectorRectangle(3, 3, 6, 6)
};

Draw(vectorObjects);
Draw(vectorObjects);


static void Draw(IEnumerable<VectorObject> vectorObjects)
{
    foreach (var vo in vectorObjects)
    {
        foreach (var line in vo)
        {
            var adapter = new LineToPointAdapter(line);
            adapter.ToList().ForEach(DrawPoint);
        }
    }
}

static void DrawPoint(Point point)
{
    Console.Write(".");
}

#region Adapter
// Build an adpater to convert vector to point
public class LineToPointAdapter : IEnumerable<Point>
{
    public static int count;
    private static Dictionary<int, List<Point>> cache = new Dictionary<int, List<Point>>();

    public LineToPointAdapter(Line line)
    {
        var hash = line.GetHashCode();
        if (cache.ContainsKey(hash)) return;

        WriteLine($"{++count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");

        var points = new List<Point>();

        int left = Math.Min(line.Start.X, line.End.X);
        int right = Math.Max(line.Start.X, line.End.X);
        int top = Math.Min(line.Start.Y, line.End.Y);
        int bottom = Math.Max(line.Start.Y, line.End.Y);
        int dx = right - left;
        int dy = line.End.Y - line.Start.Y;

        if (dx == 0)
        {
            for (int y = top; y <= bottom; ++y)
            {
                points.Add(new Point(left, y));
            }
        }
        else if (dy == 0)
        {
            for (int x = left; x <= right; ++x)
            {
                points.Add(new Point(x, top));
            }
        }

        cache.Add(hash, points);
    }

    public IEnumerator<Point> GetEnumerator() => cache.Values.SelectMany(x => x).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
#endregion

public class VectorRectangle : VectorObject
{
    public VectorRectangle(int x, int y, int width, int height)
    {
        Add(new Line(new Point(x, y), new Point(x + width, y)));
        Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
        Add(new Line(new Point(x, y), new Point(x, y + height)));
        Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
    }
}

public class VectorObject : Collection<Line>
{

}

public class Line : IEquatable<Line?>
{
    public Point Start, End;

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Line);
    }

    public bool Equals(Line? other)
    {
        return other != null &&
               EqualityComparer<Point>.Default.Equals(Start, other.Start) &&
               EqualityComparer<Point>.Default.Equals(End, other.End);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }
}

public class Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }
}