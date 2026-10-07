using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Labb_2_DungeonCrawler.Entities.Enemies
{
    abstract class Enemy : LevelEntity
    {
        public string Name { get; set; }

        public int HitPoints { get; set; }

        public Enemy(int x, int y) : base(x, y)
        {
            
        }

        public abstract void Update();

    }
}
