using System;
using System.Collections.Generic;
using System.Text;
using Labb_2_DungeonCrawler.Entities;

namespace Labb_2_DungeonCrawler
{
    public abstract class LevelEntity
    {
        public LevelEntity(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public char LevelObject { get; set; }

        public ConsoleColor Color { get; set; }

        public virtual bool IsPassable => true;

        public virtual bool IsVisible {  get; set; }

        public void Draw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = Color;
            Console.Write(IsVisible ? LevelObject: ' ');
            Console.ResetColor();
        }

        public bool ValidMove(List<LevelEntity>myEntities, int x, int y)
        {
            foreach (var entity in myEntities)
            {
                if (ReferenceEquals(entity, this)) continue;
                if (entity.X == x && entity.Y == y && !entity.IsPassable)
                {
                    return false;
                }
            }
            return true;
        }

        public void DrawMovement(char sprite, int x, int y)
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = this.Color;

            Console.Write(" ");

            this.X = x;
            this.Y = y;

            Draw();

        }
        public int DistanceToPlayer(int entityPosX, int entityPosY, int playerPosX, int PlayerPosY)
        {
            double distance = 0;
            
            distance = Math.Sqrt(Math.Pow(Convert.ToDouble(entityPosX) - Convert.ToDouble(playerPosX), 2) + Math.Pow(Convert.ToDouble(entityPosY) - Convert.ToDouble(PlayerPosY), 2));
            
            return (int)distance;
        }
        public void WithinVisionRange(int playerPosX, int PlayerPosY, int range = 5)
        {
            IsVisible = DistanceToPlayer(this.X, this.Y, playerPosX, PlayerPosY) <= range;
        }
    }
}
