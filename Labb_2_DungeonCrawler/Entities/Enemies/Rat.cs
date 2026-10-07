using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Labb_2_DungeonCrawler.Entities.Enemies
{
    internal class Rat : Enemy
    {
        private char _sprite = 'r';

        private ConsoleColor _color = ConsoleColor.Red;

        public Rat(int x, int y) : base(x, y)
        {
            LevelObject = _sprite;
            Color = _color;
            X = x; Y = y;
        }

        public override void Update()
        {

        }
    }
}
