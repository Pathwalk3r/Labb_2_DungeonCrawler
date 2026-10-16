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

        protected static void MovingDirection(Direction direction, int x, int y, out int newX, out int newY)
        {
            newX = x;
            newY = y;

            switch (direction)
            {
                case Direction.North:
                    newX--;
                    break;
                case Direction.South:
                    newX++;
                    break;
                case Direction.West:
                    newY--;
                    break;
                case Direction.East:
                    newY++;
                    break;
            }
        }

        public void EnemyAttack(Player player, int yPos)
        {
            int enemyAttack = this.AttackDice.Throw();
            int playerDefence = player.DefenseDice.Throw();

            Console.SetCursorPosition(0, yPos);
            Console.WriteLine($"{this.Name} attacked with {AttackDice.ToString()} and deal {enemyAttack} damage.".PadRight(80));
            

            int damage = enemyAttack - playerDefence;

            if (damage > 0)
            {
                player.HitPoints -= damage;
                Console.WriteLine($"{player.Name} takes {damage} damage and has {player.HitPoints} HP remaining".PadRight(80));
            }
            else
            {
                Console.WriteLine("You have blocked the damage".PadRight(80));
            }
            
        }

        public void EnemyKilled(Enemy enemy)
        {
            enemy.IsAlive = false;
        }
    }
}
