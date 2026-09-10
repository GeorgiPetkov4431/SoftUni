using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Contracts
{
    public interface ISnakeRenderer
    {
        void Initialize(int maxWidth, int maxHeight);

        void RenderWalls(int maxWidth, int maxHeight);

        void RenderSnake(Snake snake, Point toRemove = null);

        void RenderFood(Point food);

        void RenderGameOver();
    }
}
