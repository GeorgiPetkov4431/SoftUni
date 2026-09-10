using Snake.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class ConsoleSnakeGameInput : ISnakeGameInput
    {
        public Direction? CheckForInput()
        {
            if (!Console.KeyAvailable)
            {
                return null;
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                return Direction.Up;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                return Direction.Down;
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                return Direction.Left;
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                return Direction.Right;
            }

            return null;
        }

        public bool WaitForRestart()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.Enter)
                {
                    return true;
                }
            }
        }
    }
}
