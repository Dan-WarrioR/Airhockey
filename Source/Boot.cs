using Source.Core;

namespace Source
{
    public class Boot
    {
        static void Main(string[] args)
        {
            Game game = new();

            game.StartGame();
        }
    }
}