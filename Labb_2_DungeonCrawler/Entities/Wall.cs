using System;

namespace Labb_2_DungeonCrawler.Entities
{
    internal class Wall : LevelEntity
    {
        private char _sprite = '#';
        private ConsoleColor _color = ConsoleColor.White;

        public override bool IsPassable => false;

        public bool Discovered { get; set; }

        public override bool IsVisible {  get; set; }

        internal Wall(int xCord, int yCord) : base(xCord, yCord)
        {
            LevelObject = _sprite;
            Color = _color;
            XCord = xCord; YCord = yCord;
        }

        public void HasBeenDiscovered()
        {
            if (IsVisible == true)
            {
                this.Discovered = true;
            }
            if (Discovered == true)
            {
                this.IsVisible = true;
            }
        }

    }
}
