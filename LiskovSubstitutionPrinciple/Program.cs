using static System.Console;

Rectangle rc = new Rectangle(2, 3);
WriteLine($"{rc} has area {Area(rc)}");

//Square sq = new Square();
//sq.Width = 4;
//WriteLine($"{sq} has area {Area(sq)}");

Rectangle sq = new Square();
sq.Width = 4;
WriteLine($"{sq} has area {Area(sq)}");

static int Area(Rectangle r) => r.Width * r.Height;

public class Rectangle
{
    public Rectangle()
    {
    }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public override string ToString()
    {
        return $"{nameof(Width)}: {Width} {nameof(Height)}: {Height}";
    }

    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
}

public class Square : Rectangle
{
    public Square() : base()
    {
    }

    public Square(int width, int height) : base(width, height)
    {
    }

    #region Do
    public override int Width { set { base.Width = base.Height = value; } }
    public override int Height { set { base.Width = base.Height = value; } }
    #endregion

    #region Don't
    //public new int Width
    //{
    //    set { base.Width = base.Height = value; }
    //}

    //public new int Height
    //{
    //    set { base.Width = base.Height = value; }
    //}
    #endregion
}