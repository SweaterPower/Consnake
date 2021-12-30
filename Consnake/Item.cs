using System;
using System.Collections.Generic;
using System.Text;

namespace Consnake
{
    class Item
    {
        Point point;
        int foodValue;
        int scoreValue;

        public Item(Point point, int foodValue, int scoreValue)
        {
            this.Point = point;
            this.FoodValue = foodValue;
            this.ScoreValue = scoreValue;
        }

        public int FoodValue { get => foodValue; set => foodValue = value; }
        public int ScoreValue { get => scoreValue; set => scoreValue = value; }
        internal Point Point { get => point; set => point = value; }
    }
}
