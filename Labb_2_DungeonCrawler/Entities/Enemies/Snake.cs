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

        public Dice _attackDice = new Dice(numberOfDice:3, sidesPerDice:4, modifier:2);

        public Dice _defenseDice = new Dice(numberOfDice:1, sidesPerDice:8, modifier:5);

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

            Player? player = null;

            foreach (LevelEntity entity in entities.OfType<Player>())
            {
                player = entity as Player;
            }

            if (DistanceToPlayer(player) < 3)
            {
                if (player.XCord < this.XCord)
                {
                    direction = Direction.East;
                }
                if (player.XCord > this.XCord)
                {
                    direction = Direction.West;
                }
                if (player.YCord < this.YCord)
                {
                    direction = Direction.South;
                }
                if (player.YCord >  this.YCord)
                {
                    direction = Direction.North;
                }
            }

            WithinVisionRange(player);

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
