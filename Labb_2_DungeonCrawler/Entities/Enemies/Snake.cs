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

        public Snake(int x, int y) : base(x, y)
        {
            LevelObject = _sprite;
            Color = _color;
            X = x; Y = y;

            this.AttackDice = _attackDice;
            this.DefenseDice = _defenseDice;
            this.Name = _name;
            this.HitPoints = _hp;
            this.IsAlive = true;
        }
        public override void Update(List<LevelEntity> entities)
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = this.Color;

            Direction? direction = null;

            int playerPosX = 0;
            int playerPosY = 0;

            foreach (LevelEntity entity in entities)
            {
                if (entity is Player p)
                {
                    playerPosX = p.X;
                    playerPosY = p.Y;

                }
            }

            if (DistanceToPlayer(this.X, this.Y, playerPosX, playerPosY) < 3)
            {
                if (playerPosX < this.X)
                {
                    direction = Direction.South;
                }
                if (playerPosX > this.X)
                {
                    direction = Direction.North;
                }
                if (playerPosY < this.Y)
                {
                    direction = Direction.East;
                }
                if (playerPosY >  this.Y)
                {
                    direction = Direction.West;
                }
            }

            WithinVisionRange(playerPosX, playerPosY);

            int newX = this.X;
            int newY = this.Y;

            if (direction.HasValue)
            {
                MovingDirection(direction.Value, this.X, this.Y, out newX, out newY);
            }


            if (!ValidMove(entities, newX, newY))
            {
                return;
            }
            
            DrawMovement(_sprite, newX, newY);
        } 
    }
}
