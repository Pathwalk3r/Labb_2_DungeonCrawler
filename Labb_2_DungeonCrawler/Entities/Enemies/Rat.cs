using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Labb_2_DungeonCrawler.Entities.Enemies
{
    internal class Rat : Enemy
    {
        private char _sprite = 'r';

        private string _name = "Rat";

        private int _hp = 10;

        private static readonly Random _rand = new Random();

        private ConsoleColor _color = ConsoleColor.Red;

        private Dice _attackDice = new Dice(numberOfDice:1, sidesPerDice:6, modifier:3);

        private Dice _defenseDice = new Dice(numberOfDice:1, sidesPerDice:6, modifier:1);

        public Rat(int xCord, int yCord) : base(xCord, yCord)
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
            Player? player = null;

            foreach (LevelEntity entity in entities.OfType<Player>())
            {
                player = entity as Player;
            }

            Direction direction = (Direction)_rand.Next(0, 4);

            MovingDirection(direction, this.XCord, this.YCord, out int newXCord, out int newYCord);

            if (!ValidMove(entities, newXCord, newYCord))
            {
                var target = entities.FirstOrDefault(p => p.XCord == newXCord && p.YCord == newYCord);
                if (target == player)
                {
                    int yPosForWriting = 2;
                    EnemyAttack(player, yPosForWriting);
                    if (player.HitPoints > 0)
                    {
                        yPosForWriting = 3;
                        player.PlayerAttack(this, yPosForWriting);
                    }
                    
                }
                return;
            }
           
            
            WithinVisionRange(player);
            DrawMovement(_sprite, newXCord, newYCord);
        }
    }
}
