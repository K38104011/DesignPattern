var person = new Person();
person.Age = 17;
var resPerson = new ResponsiblePerson(person);
Console.WriteLine(resPerson.Drink());
Console.WriteLine(resPerson.Drive());
Console.WriteLine(resPerson.DrinkAndDrive());

public class Person
{
    public int Age { get; set; }

    public string Drink()
    {
        return "drinking";
    }

    public string Drive()
    {
        return "driving";
    }

    public string DrinkAndDrive()
    {
        return "driving while drunk";
    }
}

public class ResponsiblePerson
{
    private Person _person;

    public ResponsiblePerson(Person person)
    {
        _person = person;
    }

    public string Drink()
    {
        if (_person.Age > 18) return _person.Drink();
        return "too young";
    }

    public string Drive()
    {
        if (_person.Age > 16) return _person.Drive();
        return "too young";
    }

    public string DrinkAndDrive()
    {
        return "dead";
    }

    public int Age 
    { 
        get { return _person.Age;  } 
        set { _person.Age = value; }
    }
}
