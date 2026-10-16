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

        public Dice AttackDice;

        public Dice DefenseDice;

        public bool IsAlive {  get; set; }

        public override bool IsPassable => false;

        protected enum Direction { North, South, West, East }

        public abstract void Update(List<LevelEntity>entities);

        protected static void MovingDirection(Direction direction, int XCord, int yCord, out int newXCord, out int newYCord)
        {
            newXCord = XCord;
            newYCord = yCord;

            switch (direction)
            {
                case Direction.West:
                    newXCord--;
                    break;
                case Direction.East:
                    newXCord++;
                    break;
                case Direction.North:
                    newYCord--;
                    break;
                case Direction.South:
                    newYCord++;
                    break;
            }
        }

        public void EnemyAttack(Player player, int yPos)
        {
            int enemyAttack = this.AttackDice.Throw();
            int playerDefence = player.DefenseDice.Throw();

            string summery = String.Empty;

            
            summery += $"{this.Name} attacked with {AttackDice.ToString()} and deal {enemyAttack} damage. ";
            

            int damage = enemyAttack - playerDefence;

            if (damage > 0)
            {
                player.HitPoints -= damage;
                summery += $"{player.Name} takes {damage} damage and has {player.HitPoints} HP remaining.";

                if (this.HitPoints <= 0)
                {
                    this.EnemyKilled(this);
                    summery += $"{this.Name} has died!";
                }
            }
            else
            {
                summery += "You have blocked the damage";
            }
            WriteAtLine(yPos, summery);
        }

        public void EnemyKilled(Enemy enemy)
        {
            enemy.IsAlive = false;
            enemy.LevelObject = ' ';
            Console.SetCursorPosition(enemy.XCord, enemy.YCord);
            Console.ForegroundColor = enemy.Color;
            Console.Write(enemy.LevelObject);
            Console.ResetColor();
        }
    }
}
