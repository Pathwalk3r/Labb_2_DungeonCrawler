using System;
using System.Collections.Generic;
using System.Text;

namespace Labb_2_DungeonCrawler.Entities.Enemies
{
    internal class Snake : Enemy
    {
        private char _sprite = 's';

        private ConsoleColor _color = ConsoleColor.Green;

        public Snake(int x, int y) : base(x, y)
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
