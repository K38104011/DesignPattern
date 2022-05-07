
#region Do
public interface IScanner
{
    void Scan(Document document);
}

public interface IPrinter
{
    void Print(Document document);
}

public class Photocopier : IPrinter, IScanner
{
    public void Print(Document document)
    {
        Console.WriteLine("Print");
    }

    public void Scan(Document document)
    {
        Console.WriteLine("Scan");
    }
}

public interface IMultiFunctionDevice : IScanner, IPrinter
{

}

//Decorator Pattern
public class MultiFunctionDevice : IMultiFunctionDevice
{
    private IPrinter printer;
    private IScanner scanner;

    public MultiFunctionDevice(IPrinter printer, IScanner scanner)
    {
        this.printer = printer;
        this.scanner = scanner;
    }

    public void Print(Document document)
    {
        printer.Print(document);
    }

    public void Scan(Document document)
    {
        scanner.Scan(document);
    }
}
#endregion

#region Don't
public class OldFashionedPrinter : IMachine
{
    public void Fax(Document document)
    {
        Console.WriteLine("Fax");
    }

    public void Print(Document document)
    {
        throw new NotImplementedException();
    }

    public void Scan(Document document)
    {
        throw new NotImplementedException();
    }
}

public class MultiFunctionPrinter : IMachine
{
    public void Fax(Document document)
    {
        Console.WriteLine("Fax");
    }

    public void Print(Document document)
    {
        Console.WriteLine("Print");
    }

    public void Scan(Document document)
    {
        Console.WriteLine("Scan");
    }
}

public interface IMachine
{
    void Print(Document document);
    void Scan(Document document);
    void Fax(Document document);
}
#endregion

public class Document { }