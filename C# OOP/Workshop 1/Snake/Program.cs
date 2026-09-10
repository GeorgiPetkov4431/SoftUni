using Snake.Contracts;

namespace Snake
{
    public class Program
    {
        static void Main()
        {
            ISnakeRenderer renderer = new ConsoleSnakeGameRenderer();
            ISnakeGameInput input = new ConsoleSnakeGameInput();
            IRandomGenerator randomGenerator = new RandomGenerator();
            SnakeGameEngine gameEngine = new SnakeGameEngine(renderer, input, randomGenerator);

            gameEngine.Run();
        }
    }
}
