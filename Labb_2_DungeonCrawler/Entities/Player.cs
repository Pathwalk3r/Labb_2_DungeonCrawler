using System;
using System.Collections.Generic;
using System.Text;

namespace Labb_2_DungeonCrawler.Entities
{
    internal class Player : LevelEntity
    {
        private char _sprite = '@';

        private ConsoleColor _color = ConsoleColor.Yellow;

        public int HitPoints { get; set; }

        public Player(int x,  int y) : base(x, y)
        {
            LevelObject = _sprite;
            Color = _color;
        }
    }
}
