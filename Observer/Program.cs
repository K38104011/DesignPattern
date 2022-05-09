var person = new Person();
person.FallsIll += CallDoctor;
person.CatchACold();
person.FallsIll -= CallDoctor;
person.CatchACold();

void CallDoctor(object? sender, FallsIllEventArgs e)
{
    Console.WriteLine($"A doctor has been called to {e.Address}");
}

public class FallsIllEventArgs
{
    public string Address;
}

public class Person
{
    public event EventHandler<FallsIllEventArgs> FallsIll;

    public void CatchACold()
    {
        FallsIll?.Invoke(this, new FallsIllEventArgs { Address = "177 Bui Huu Nghia" });
    }
}