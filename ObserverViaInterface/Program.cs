var person = new Person();
var sub1 = person.Subscribe(new PersonHandler());
var sub2 = person.Subscribe(new PersonHandler());
person.FallsIll();

sub1.Dispose();sub2.Dispose();

public class Event { }
public class FallsIllEvent : Event
{
    public string Address { get; set; }
}

public class Person : IObservable<Event>
{
    private readonly HashSet<Subscription> subscriptions = new HashSet<Subscription>();

    public IDisposable Subscribe(IObserver<Event> observer)
    {
        var sub = new Subscription(this, observer);
        subscriptions.Add(sub);
        return sub;
    }

    public void FallsIll()
    {
        foreach (var sub in subscriptions)
        {
            sub.Observer.OnNext(new FallsIllEvent { Address = "177/20 Bui Huu Nghia" });
        }
    }

    private class Subscription : IDisposable
    {
        private readonly Person person;

        public IObserver<Event> Observer { get; }

        public Subscription(Person person, IObserver<Event> observer)
        {
            this.person = person;
            Observer = observer;
        }

        public void Dispose()
        {
            person.subscriptions.Remove(this);
        }
    }
}

public class PersonHandler : IObserver<Event>
{
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(Event value)
    {
        if (value is FallsIllEvent args)
            Console.WriteLine($"A doctor is required at {args.Address}");
    }
}