using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Labb_2_DungeonCrawler.Entities.Enemies
{
    internal class Snake : Enemy
    {
        private char _sprite = 's';

        private string _name = "Snake";

        private int _hp = 25;

        private ConsoleColor _color = ConsoleColor.Green;

        public Dice _attackDice = new Dice(3, 4, 2);

        public Dice _defenseDice = new Dice(1, 8, 5);

        public Snake(int xCord, int yCord) : base(xCord, yCord)
        {
            LevelObject = _sprite;
            Color = _color;
            XCord = xCord; YCord = yCord;

            this.AttackDice = _attackDice;
            this.DefenseDice = _defenseDice;
            this.Name = _name;
            this.HitPoints = _hp;
            this.IsAlive = true;
        }
        public override void Update(List<LevelEntity> entities)
        {
            Console.SetCursorPosition(this.XCord, this.YCord);
            Console.ForegroundColor = this.Color;

            Direction? direction = null;

            int playerPosX = 0;
            int playerPosY = 0;

            foreach (LevelEntity entity in entities)
            {
                if (entity is Player p)
                {
                    playerPosX = p.XCord;
                    playerPosY = p.YCord;

                }
            }

            if (DistanceToPlayer(this.XCord, this.YCord, playerPosX, playerPosY) < 3)
            {
                if (playerPosX < this.XCord)
                {
                    direction = Direction.East;
                }
                if (playerPosX > this.XCord)
                {
                    direction = Direction.West;
                }
                if (playerPosY < this.YCord)
                {
                    direction = Direction.South;
                }
                if (playerPosY >  this.YCord)
                {
                    direction = Direction.North;
                }
            }

            WithinVisionRange(playerPosX, playerPosY);

            int newXCord = this.XCord;
            int newYCord = this.YCord;

            if (direction.HasValue)
            {
                MovingDirection(direction.Value, this.XCord, this.YCord, out newXCord, out newYCord);
            }


            if (!ValidMove(entities, newXCord, newYCord))
            {
                return;
            }
            
            DrawMovement(_sprite, newXCord, newYCord);
        } 
    }
}
