namespace FactoriesPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new PersonFactory();
            Console.WriteLine(factory.CreatePerson("first").Id);
            Console.WriteLine(factory.CreatePerson("second").Id);
        }
    }

    public class PersonFactory
    {
        private int _index = 0;

        public Person CreatePerson(string personName)
        {
            return new Person
            {
                Id = _index++,
                Name = personName,
            };
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}