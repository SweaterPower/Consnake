using System;
using System.Collections.Generic;
using System.Text;

namespace Consnake
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    static class Game
    {
        static public Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down: return Direction.Up;
                case Direction.Up: return Direction.Down;
                case Direction.Left: return Direction.Right;
                case Direction.Right: return Direction.Left;
                default: return Direction.None;
            }
        }

        static public Point MakeStepInDirection(Point start, Direction direction)
        {
            switch (direction)
            {
                case Direction.Down: return new Point(start.X, start.Y+1);
                case Direction.Up: return new Point(start.X, start.Y - 1);
                case Direction.Left: return new Point(start.X - 1, start.Y);
                case Direction.Right: return new Point(start.X + 1, start.Y);
                default: return start;
            }
        }

        static public Snake snake;
        static Screen screen;
        static Item item;
        static int playerScore;

        static public void StartNewGame(int screenWidth, int screenHeight)
        {
            playerScore = 0;
            snake = new Snake();
            ResetSnake(screenWidth, screenHeight);
            screen = new Screen(snake, null, screenWidth, screenHeight);
            item = GenerateRandomItem(screen);
            screen.Draw();
        }

        static public void ResetGame()
        {
            playerScore = 0;
            ResetSnake(screen.Width, screen.Height);
            item = GenerateRandomItem(screen);
            screen.Draw();
        }

        static void ResetSnake(int screenWidth, int screenHeight)
        {
            SnakeNode head = new SnakeNode(Direction.Right);
            head.Point = new Point(screenWidth / 2, screenHeight / 2);
            snake.HeadNode = head;
            snake.TailNode = head;
            snake.AddNode();
            snake.AddNode();
        }

        static Item GenerateRandomItem(Screen frame)
        {
            Random rnd = new Random();
            int x = rnd.Next(1, frame.Width);
            int y = rnd.Next(1, frame.Height);
            int food = rnd.Next(1, 10);
            int value = food * 100;
            Item item = new Item(new Point(x,y), food, value);
            frame.UpdateItem(item);
            return item;
        }

        //возвращает, врезался ли игрок в стену или в себя
        static public bool gamePlay(Direction playerCommand)
        {
            //сохраняем положение хвоста
            Point oldTail = new Point(snake.TailNode.Point.X, snake.TailNode.Point.Y);
            //двигаемся в выбранном направлении
            snake.Move(playerCommand);
            //проверка на столкновение с препятствием
            Point head = snake.HeadNode.Point;
            if (head.X == 0 || head.X == screen.Width ||
                head.Y == 0 || head.Y == screen.Height)
            {
                return true;
            }
            SnakeNode node = snake.HeadNode;
            while (node.Previous != null)
            {
                SnakeNode previous = node.Previous;
                if (snake.HeadNode.Point.X == previous.Point.X &&
                    snake.HeadNode.Point.Y == previous.Point.Y)
                {
                    return true;
                }
                node = previous;
            }
            //проверка на получение еды
            if (snake.HeadNode.Point.X == item.Point.X &&
                snake.HeadNode.Point.Y == item.Point.Y)
            {
                playerScore += item.ScoreValue;
                for (int i = 1; i <= item.FoodValue; i++)
                {
                    snake.AddNode();
                }
                item = GenerateRandomItem(screen);
            }
            //перерисовка игрового поля
            screen.Redraw(playerScore, oldTail, snake.HeadNode);

            return false;
        }
    }
}
