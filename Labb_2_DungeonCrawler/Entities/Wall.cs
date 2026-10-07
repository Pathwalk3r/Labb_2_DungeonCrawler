using System;

namespace Labb_2_DungeonCrawler.Entities
{
    internal class Wall : LevelEntity
    {
        private char _sprite = '#';
        private ConsoleColor _color = ConsoleColor.White;

        internal Wall(int x, int y) : base(x, y)
        {
            LevelObject = _sprite;
            Color = _color;
            X = x; Y = y;
        }

    }
}
