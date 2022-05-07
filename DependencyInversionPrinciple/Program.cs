var parent = new Person { Name = "John" };
var child1 = new Person { Name = "Chris" };
var child2 = new Person { Name = "Mary" };

var relationships = new Relationships();
relationships.AddParentAndChild(parent, child1);
relationships.AddParentAndChild(parent, child2);

new Research((IRelationshipBrowser)relationships);

public enum Relationship
{
    Parent,
    Child,
    Sibling
}

public class Person
{
    public string Name { get; set; }
}

#region Do
public interface IRelationshipBrowser
{
    IEnumerable<Person> FindAllChildrenOf(string name);
}
#endregion

//low-level
public class Relationships : IRelationshipBrowser
{
    private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)> ();

    public void AddParentAndChild(Person parent, Person child)
    {
        relations.Add((parent, Relationship.Parent, child));
        relations.Add((child, Relationship.Child, parent));
    }

    #region Do
    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
        foreach (var r in relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent))
        {
            yield return r.Item3;
        }
    }
    #endregion

    #region Don't
    public List<(Person, Relationship, Person)> Relations => relations;
    #endregion
}

//high-level
public class Research
{
    #region
    public Research(IRelationshipBrowser browser)
    {
        foreach (var p in browser.FindAllChildrenOf("John"))
        {
            Console.WriteLine($"John has a chldren called {p.Name}");
        }
    }
    #endregion

    #region Don't
    public Research(Relationships relationships)
    {
        var relations = relationships.Relations;
        foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
        {
            Console.WriteLine($"John has a child called {r.Item3.Name}");
        }
    }
    #endregion

}