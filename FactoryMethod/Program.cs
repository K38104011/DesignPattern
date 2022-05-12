var point1 = Point.Factory.NewCartesianPoint(1, 1);
             //Task.Factory.StartNew()

Console.WriteLine(point1);
var point2 = Point.Factory.NewPolarPoint(1, 1);
Console.WriteLine(point2);

Console.WriteLine(Point.WrongOrigin == Point.WrongOrigin);      //always create new object
Console.WriteLine(Point.CorrectOrigin == Point.CorrectOrigin);  //initialize once


public class Point
{
    #region Factory Method
    //public static Point NewCartesianPoint(double x, double y)
    //{
    //    return new Point(x, y);
    //}

    //public static Point NewPolarPoint(double rho, double theta)
    //{
    //    return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    //}
    #endregion

    private double x, y;
    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{x}, {y}";
    }

    public static Point WrongOrigin => new Point(0, 0);
    public static Point CorrectOrigin = new Point(0, 0);


    //Separation of concern
    #region Factory
    public static class Factory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }
    #endregion
}