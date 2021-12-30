using System;
using System.Collections.Generic;
using System.Text;

namespace Consnake
{
    class Screen
    {
        char cborderUD = '═';
        char cborderLR = '║';
        char cborderCornerLU = '╔';
        char cborderCornerLD = '╚';
        char cborderCornerRU = '╗';
        char cborderCornerRD = '╝';
        char csnakeNodeL = '┤';
        char csnakeNodeR = '├';
        char csnakeNodeU = '┴';
        char csnakeNodeD = '┬';
        char cemptySpace = ' ';

        public void SetGraphics(char[] tiles)
        {
            if (tiles.Length >= 11)
            {
                cborderUD = tiles[0];
                cborderLR = tiles[1];
                cborderCornerLU = tiles[2];
                cborderCornerLD = tiles[3];
                cborderCornerRU = tiles[4];
                cborderCornerRD = tiles[5];
                csnakeNodeL = tiles[6];
                csnakeNodeR = tiles[7];
                csnakeNodeU = tiles[8];
                csnakeNodeD = tiles[9];
                cemptySpace = tiles[10];
            }
        }

        int width;
        int height;
        Snake snake;
        Item item;

        public Screen(Snake snake, Item item, int width, int height)
        {
            this.snake = snake;
            this.item = item;
            Width = width;
            Height = height;
        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public void UpdateItem(Item newItem)
        {
            this.item = newItem;
        }

        public void Draw()
        {
            //удаление всех символов с экрана
            Console.Clear();
            //отрисовка торцевых сторон игрового поля
            Console.CursorTop = 0;
            for (int i = 1; i < Width; i++)
            {
                Console.CursorLeft = i;
                Console.Write(cborderUD);
            }
            Console.CursorTop = Height;
            for (int i = 1; i < Width; i++)
            {
                Console.CursorLeft = i;
                Console.Write(cborderUD);
            }
            //отрисовка боковых сторон игрового поля
            for (int i = 1; i < Height; i++)
            {
                Console.CursorLeft = 0;
                Console.CursorTop = i;
                Console.Write(cborderLR);
            }
            Console.CursorLeft = Width;
            for (int i = 1; i < Height; i++)
            {
                Console.CursorLeft = Width;
                Console.CursorTop = i;
                Console.Write(cborderLR);
            }
            //отрисовка углов игрового поля
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            Console.Write(cborderCornerLU);
            Console.CursorLeft = 0;
            Console.CursorTop = Height;
            Console.Write(cborderCornerLD);
            Console.CursorLeft = Width;
            Console.Write(cborderCornerRD);
            Console.CursorTop = 0;
            Console.CursorLeft = Width;
            Console.Write(cborderCornerRU);
            //отрисовка змейки
            SnakeNode node = snake.HeadNode;
            while (node != null)
            {
                Console.CursorLeft = node.Point.X;
                Console.CursorTop = node.Point.Y;
                DrawNode(node);
                //Console.CursorLeft = width + 5;
                //Console.Write(String.Format("( {0} ; {1} )", node.Point.X, node.Point.Y));
                node = node.Previous;
            }
        }

        public void Redraw(int score, Point oldTail, SnakeNode newHead)
        {
            //отрисовка еды для змейки
            Console.CursorLeft = item.Point.X;
            Console.CursorTop = item.Point.Y;
            Console.Write(item.FoodValue);

            //удаление хвоста змейки
            Console.CursorLeft = oldTail.X;
            Console.CursorTop = oldTail.Y;
            Console.Write(cemptySpace);

            //отрисовка головы змейки
            Console.CursorLeft = newHead.Point.X;
            Console.CursorTop = newHead.Point.Y;
            DrawNode(newHead);

            //отрисовка игрового счета
            Console.CursorLeft = 0;
            Console.CursorTop = Height + 1;
            Console.Write("Score: " + score);
        }

        void DrawNode(SnakeNode node)
        {
            switch (node.Direction)
            {
                case Direction.Down: Console.Write(csnakeNodeD); break;
                case Direction.Up: Console.Write(csnakeNodeU); break;
                case Direction.Left: Console.Write(csnakeNodeL); break;
                case Direction.Right: Console.Write(csnakeNodeR); break;
            }
        }
    }
}
