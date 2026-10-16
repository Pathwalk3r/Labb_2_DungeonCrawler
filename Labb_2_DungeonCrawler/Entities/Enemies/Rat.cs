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

        private Dice _attackDice = new Dice(1, 6, 3);

        private Dice _defenseDice = new Dice(1, 6, 1);

        public Rat(int x, int y) : base(x, y)
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
            Direction direction = (Direction)_rand.Next(0, 4);

            MovingDirection(direction, this.X, this.Y, out int newX, out int newY);

            if (!ValidMove(entities, newX, newY))
            {
                var target = entities.FirstOrDefault(p => p.X == newX && p.Y == newY);
                if (target is Player player)
                {
                    int yPos = 1;
                    EnemyAttack(player, yPos);
                    if (player.HitPoints > 0)
                    {
                        yPos = 2;
                        player.PlayerAttack(this, yPos);
                    }
                    
                }
                return;
            }
                
                

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
            WithinVisionRange(playerPosX, playerPosY);
            DrawMovement(_sprite, newX, newY);
        }
    }
}
