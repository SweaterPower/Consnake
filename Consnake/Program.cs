using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consnake
{
    class Program
    {
        static void Main(string[] args)
        {
            int gameDelay = 200;
            bool isGameOver = false;
            Direction direction = Direction.Right;
            Game.StartNewGame(40, 20);
            Task task = new Task(new Action(() => 
                {
                    while (!isGameOver)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow: 
                                if (Game.snake.HeadNode.Direction != Direction.Down)
                                    direction = Direction.Up; break;
                            case ConsoleKey.DownArrow:
                                if (Game.snake.HeadNode.Direction != Direction.Up)
                                    direction = Direction.Down; break;
                            case ConsoleKey.LeftArrow:
                                if (Game.snake.HeadNode.Direction != Direction.Right) 
                                    direction = Direction.Left; break;
                            case ConsoleKey.RightArrow:
                                if (Game.snake.HeadNode.Direction != Direction.Left)
                                    direction = Direction.Right; break;
                        }
                    }
                }
                ), new CancellationToken(isGameOver));
            task.Start();
            while (!Game.gamePlay(direction))
            {
                Thread.Sleep(gameDelay);
            }
            isGameOver = true;
            Console.Clear();
            Console.WriteLine("Game Over!");
        }
    }
}
