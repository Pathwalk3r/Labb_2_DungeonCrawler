using System;
using System.Collections.Generic;
using System.Text;

namespace Labb_2_DungeonCrawler
{
    public abstract class LevelEntity
    {
        public LevelEntity(int x, int y)
        {
            X = x; Y = y;
        }
        public int X {  get; set; }
        public int Y { get; set; }

        public char LevelObject { get; set; }

        public ConsoleColor Color { get; set; }

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Color;
            Console.Write(LevelObject);
            Console.ResetColor();
        }
    }
}
