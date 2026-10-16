using System;
using System.Collections.Generic;
using System.Text;
using Labb_2_DungeonCrawler.Entities;

namespace Labb_2_DungeonCrawler
{
    public abstract class LevelEntity
    {
        public LevelEntity(int xCord, int yCord)
        {
            XCord = xCord;
            YCord = yCord;
        }

        public int XCord { get; set; }
        public int YCord { get; set; }

        public char LevelObject { get; set; }

        public ConsoleColor Color { get; set; }

        public virtual bool IsPassable => true;

        public virtual bool IsVisible {  get; set; }

        public void Draw()
        {
            Console.SetCursorPosition(this.XCord, this.YCord);
            Console.ForegroundColor = Color;
            Console.Write(IsVisible ? LevelObject: ' ');
            Console.ResetColor();
        }

        public bool ValidMove(List<LevelEntity>myEntities, int xCord, int yCord)
        {
            foreach (var entity in myEntities)
            {
                if (ReferenceEquals(entity, this)) continue;
                if (entity.XCord == xCord && entity.YCord == yCord && !entity.IsPassable)
                {
                    return false;
                }
            }
            return true;
        }

        public void DrawMovement(char sprite, int xCord, int yCord)
        {
            Console.SetCursorPosition(this.XCord, this.YCord);
            Console.ForegroundColor = this.Color;

            Console.Write(" ");

            this.XCord = xCord;
            this.YCord = yCord;

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
            IsVisible = DistanceToPlayer(this.XCord, this.YCord, playerPosX, PlayerPosY) <= range;
        }

        public static void WriteAtLine (int row, string text)
        {
            Console.SetCursorPosition(0, row);
            Console.Write(text.PadRight(Console.WindowWidth));
        }
    }
}
