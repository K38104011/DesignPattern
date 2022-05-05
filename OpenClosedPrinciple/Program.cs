using static System.Console;

var apple = new Product("Apple", Color.Green, Size.Small);
var tree = new Product("Tree", Color.Green, Size.Large);
var house = new Product("House", Color.Blue, Size.Large);

Product[] products = { apple, tree, house };
var pf = new ProductFilter();
WriteLine("Green produts (old):");
foreach(var p in pf.FilterByColor(products, Color.Green))
{
    WriteLine($" - {p.Name} is green");
}

var bf = new BetterFilter();
WriteLine("Green products (new):");
foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
{
    WriteLine($" - {p.Name} is green");
}

WriteLine("Large blue items");
foreach (var p in bf.Filter(products, 
    new AndSpecification<Product>(
        new ColorSpecification(Color.Blue), 
        new SizeSpecification(Size.Large) 
        )
    ))
{
    WriteLine($" - {p.Name} is large and blue");
}

public enum Color
{
    Red, Green, Blue
}

public enum Size
{
    Small, Medium, Large, Yuge
}

public class Product
{
    public string Name;
    public Color Color;
    public Size Size;

    public Product(string name, Color color, Size size)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Color = color;
        Size = size;
    }
}

#region Don't
public class ProductFilter
{
    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
        foreach (Product product in products)
        {
            if (product.Size == size)
                yield return product;
        }
    }

    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
        foreach (Product product in products)
        {
            if (product.Color == color)
                yield return product;
        }
    }

    public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
    {
        foreach (Product product in products)
        {
            if (product.Size == size && product.Color == color)
                yield return product;
        }
    }
}
#endregion

#region Do
public interface ISpecification<T>
{
    bool IsSatisfied(T t);
}

public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}

public class ColorSpecification : ISpecification<Product>
{
    private Color color;

    public ColorSpecification(Color color)
    {
        this.color = color;
    }

    public bool IsSatisfied(Product t)
    {
        return t.Color == color;
    }
}

public class SizeSpecification : ISpecification<Product>
{
    private Size size;

    public SizeSpecification(Size size)
    {
        this.size = size;
    }

    public bool IsSatisfied(Product t)
    {
        return t.Size == size;
    }
}

public class AndSpecification<T> : ISpecification<T>
{
    private ISpecification<T> first, second;

    public AndSpecification(ISpecification<T> first, ISpecification<T> second)
    {
        this.first = first ?? throw new ArgumentNullException(nameof(first));
        this.second = second ?? throw new ArgumentNullException(nameof(second));
    }

    public bool IsSatisfied(T t)
    {
        return first.IsSatisfied(t) && second.IsSatisfied(t);
    }
}

public class BetterFilter : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
    {
        foreach (var item in items)
        {
            if (spec.IsSatisfied(item))
                yield return item;
        }
    }
}
#endregion
