using Snake.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakeGameEngine
    {
        private const int InitialGameSpeed = 100;

        private const int PlayAreaWidth = 61;
        private const int PlayAreaHeight = 30;

        private const int SnakeStartLeft = 7;
        private const int SnakeStartTop = 1;
        private const int SnakeStartLength = 6;

        private ISnakeRenderer renderer;
        private ISnakeGameInput input;
        private IRandomGenerator randomGenerator;

        private Snake snake;
        private Point availableFood;

        private int gameSpeed;

        public SnakeGameEngine(
            ISnakeRenderer renderer,
            ISnakeGameInput input,
            IRandomGenerator randomGenerator)
        {
            this.renderer = renderer;
            this.input = input;
            this.randomGenerator = randomGenerator;
        }

        public void Run()
        {
            // 1. Initialize & draw game - wall, snake, food, score, etc.
            this.InitializeGame();

            while (true)
            {
                Thread.Sleep(this.gameSpeed);

                // 2. Move snake - right direction
                Point removedPoint = this.snake.Move();

                // 3. Check key inputs and change direction accordingly
                Direction? inputDirection = this.input.CheckForInput();
                if (inputDirection != null)
                {
                    this.snake.ChangeDirection(inputDirection.Value);
                }

                // 4. Check collisions for game over - with wall and with self
                if (!this.SnakePositionIsValid())
                {
                    this.renderer.RenderGameOver();
                    break;
                }

                // 5. Check for food - increase score, snake length and game speed (optional)
                this.CheckForFood();

                // 6. On game over, save high score
                // TODO

                this.renderer.RenderSnake(this.snake, removedPoint);
            }

            if (this.input.WaitForRestart())
            {
                this.Run();
            }
        }
        
        public void InitializeGame()
        {
            this.gameSpeed = InitialGameSpeed;

            this.snake = new Snake(SnakeStartLeft, SnakeStartTop, SnakeStartLength);

            this.renderer.Initialize(PlayAreaWidth, PlayAreaHeight);
            this.renderer.RenderWalls(PlayAreaWidth, PlayAreaHeight);

            this.renderer.RenderSnake(this.snake);

            this.GenerateNewFood();
        }

        private bool SnakePositionIsValid()
        {
            int countHeadPoints = 0;

            foreach (Point point in this.snake.Body)
            {
                if (point.Left < 1 ||
                    point.Left >= PlayAreaWidth ||
                    point.Top < 1 ||
                    point.Top >= PlayAreaHeight)
                {
                    return false;
                }

                Point snakeHead = this.snake.Head;

                if (point.Left == snakeHead.Left &&
                    point.Top == snakeHead.Top)
                {
                    countHeadPoints++;
                }
            }

            if (countHeadPoints > 1)
            {
                return false;
            }

            return true;
        }

        private void CheckForFood()
        {
            Point snakeHead = this.snake.Head;

            if (this.availableFood.Left == snakeHead.Left &&
                this.availableFood.Top == snakeHead.Top)
            {
                this.snake.IncreaseLength();
                this.gameSpeed -= 5;

                this.GenerateNewFood();
            }
        }

        private void GenerateNewFood()
        {
            while (true)
            {
                bool isValidFood = true;

                int foodLeft = this.randomGenerator.NextNumber(1, PlayAreaWidth - 1);
                int foodTop = this.randomGenerator.NextNumber(1, PlayAreaHeight - 1);

                if (foodLeft % 2 == 0)
                {
                    foodLeft++;
                }

                foreach (Point point in this.snake.Body)
                {
                    if (point.Left == foodLeft && point.Top == foodTop)
                    {
                        isValidFood = false;
                        break;
                    }
                }

                if (isValidFood)
                {
                    this.availableFood = new Point(foodLeft, foodTop);
                    break;
                }
            }

            this.renderer.RenderFood(this.availableFood);
        }
    }
}
