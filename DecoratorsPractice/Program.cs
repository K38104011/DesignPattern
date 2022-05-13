public class Bird
{
    public int Age { get; set; }

    public string Fly()
    {
        return (Age < 10) ? "flying" : "too old";
    }
}

public class Lizard
{
    public int Age { get; set; }

    public string Crawl()
    {
        return (Age > 1) ? "crawling" : "too young";
    }
}

public class Dragon // no need for interfaces
{
    private Bird bird = new Bird();
    private Lizard lizard = new Lizard();
    private int age;

    public int Age { 
        get { return age; }
        set { 
            age = value;
            bird.Age = age;
            lizard.Age = age;
        }
    }

    public string Fly()
    {
        return bird.Fly();
    }

    public string Crawl()
    {
        return lizard.Crawl();
    }
}