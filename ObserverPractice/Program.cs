using Coding.Exercise;

var game = new Game();
var rat1 = new Rat(game);
var rat2 = new Rat(game);
Console.WriteLine(rat1.Attack);
Console.WriteLine(rat2.Attack);
rat1.Dispose();
Console.WriteLine(rat2.Attack);

namespace Coding.Exercise
{
    public class Game
    {
        private readonly List<Rat> _rats = new List<Rat>();

        public event EventHandler<int> RatCountChanged;

        public void AddRat(Rat rat)
        {
            _rats.Add(rat);
            RatCountChanged?.Invoke(this, _rats.Count);
        }

        public void RemoveRat(Rat rat)
        {
            _rats.Remove(rat);
            RatCountChanged?.Invoke(this, _rats.Count);
        }
    }

    public class Rat : IDisposable
    {
        public int Attack = 1;
        private Game _game;

        public Rat(Game game)
        {
            _game = game;
            _game.RatCountChanged += (sender, args) =>
            {
                Attack = args;
            };
            _game.AddRat(this);
        }

        public void Dispose()
        {
            _game.RemoveRat(this);
        }
    }
}

