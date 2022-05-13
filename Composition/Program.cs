using System.Text;

var drawing = new GraphicObject
{
    Name = "My Drawing",
    Children =
    {
        new Square
        {
            Color = "Red"
        },
        new Circle
        {
            Color = "Yellow"
        },
        new GraphicObject
        {
            Children =
            {
                new Circle
                {
                    Color = "Blue"
                },
                new Square
                {
                    Color = "Blue"
                }
            }
        }
    }
};
Console.WriteLine(drawing);

public class GraphicObject
{
    public virtual string Name { get; set; } = "Group";
    public string Color { get; set; }

    private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
    public List<GraphicObject> Children => children.Value;

    private void Print(StringBuilder sb, int depth)
    {
        sb.Append(new string('*', depth))
          .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
          .AppendLine(Name);
        foreach (var child in Children)
        {
            child.Print(sb, depth + 1);
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        Print(sb, 0);
        return sb.ToString();
    }
}

public class Circle : GraphicObject
{
    public override string Name => "Circle";
}

public class Square : GraphicObject
{
    public override string Name => "Square";
}