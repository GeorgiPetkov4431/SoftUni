using Snake.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class ConsoleSnakeGameRenderer : ISnakeRenderer
    {
        private const char WallSymbol = '#';
        private const char SnakeSymbol = '*';
        private const char FoodSymbol = '@';

        private int maxWidth;
        private int maxHeight;

        public void Initialize(int maxWidth, int maxHeight)
        {
            Console.Clear();

            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;

            Console.SetWindowSize(this.maxWidth, this.maxHeight);

            Console.CursorVisible = false;
        }

        public void RenderWalls(int maxWidth, int maxHeight)
        {
            // Top Wall
            for (int i = 0; i < maxWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(WallSymbol);
            }

            // Bottom Wall
            for (int i = 0; i < maxWidth; i++)
            {
                Console.SetCursorPosition(i, maxHeight - 1);
                Console.Write(WallSymbol);
            }

            // Left Wall
            for (int i = 0; i < maxHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(WallSymbol);
            }

            // Left Wall
            for (int i = 0; i < maxHeight; i++)
            {
                Console.SetCursorPosition(maxWidth - 1, i);
                Console.Write(WallSymbol);
            }
        }

        public void RenderSnake(Snake snake, Point toRemove = null)
        {
            if (toRemove != null)
            {
                Console.SetCursorPosition(toRemove.Left, toRemove.Top);
                Console.Write(' ');
            }

            foreach (Point point in snake.Body)
            {
                Console.SetCursorPosition(point.Left, point.Top);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(SnakeSymbol);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void RenderFood(Point food)
        {
            Console.SetCursorPosition(food.Left, food.Top);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(FoodSymbol);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void RenderGameOver()
        {
            Console.SetCursorPosition(this.maxWidth / 2 - 5, this.maxHeight / 2 - 2);
            Console.Write("GAME OVER");
            Console.SetCursorPosition(this.maxWidth / 2 - 14, this.maxHeight / 2 - 1);
            Console.Write("Press 'Enter' to restart...");
        }
    }
}
