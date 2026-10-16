using Labb_2_DungeonCrawler.Entities.Enemies;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Labb_2_DungeonCrawler.Entities
{
    internal class Player : LevelEntity
    {
        private char _sprite = '@';

        private int _hp = 100;

        private string _name = "Player";

        private ConsoleColor _color = ConsoleColor.Yellow;

        public int _maxHp = 100;

        public override bool IsPassable => false;

        public override bool IsVisible => true;

        //private List<Item> _inventory;

        public string Name { get; set; }

        public int HitPoints { get => _hp; set => _hp = value; }

        private Dice _attackDice = new Dice(2, 6, 2);

        private Dice _defenseDice = new Dice(2, 6, 0);

        public Dice AttackDice { get; set;  }

        public Dice DefenseDice { get; set; }

        public Player(int x,  int y) : base(x, y)
        {
            LevelObject = _sprite;
            Color = _color;
            
            this.AttackDice = _attackDice;
            this.DefenseDice = _defenseDice;
            this.HitPoints = _hp;
            this.Name = _name;
        }

        public bool update(ConsoleKey input, List<LevelEntity>entities)
        {
            bool gameover = false;

            int newX = this.X;
            int newY = this.Y;

            switch (input)
            {
                case ConsoleKey.LeftArrow:
                    newX--;
                    break;
                case ConsoleKey.RightArrow:
                    newX++;
                    break;
                case ConsoleKey.UpArrow:
                    newY--;
                    break;
                case ConsoleKey.DownArrow:
                    newY++;
                    break;
                case ConsoleKey.Escape:
                    gameover = true;
                    break;
            }

            if (!ValidMove(entities, newX, newY))
            {
                var target = entities.FirstOrDefault(e => e.X ==  newX && e.Y == newY);
                if (target is Enemy enemy)
                {
                    int yPos = 1;
                    PlayerAttack(enemy, yPos);
                    if (enemy.IsAlive)
                    {
                        yPos = 2;
                        enemy.EnemyAttack(this, yPos);
                    }
                }
                return false;
            }

            DrawMovement(_sprite, newX, newY);
            return gameover;
        }

        public void PlayerAttack(Enemy enemy, int yPos)
        {
            int playerAttack = this.AttackDice.Throw();
            int enemyDefense = enemy.DefenseDice.Throw();

            

            int damage = playerAttack - enemyDefense;

            Console.SetCursorPosition(0, yPos);
            Console.WriteLine($"{this.Name} attacked with {AttackDice.ToString()} and deal {playerAttack} damage.".PadRight(80));

            if (damage > 0)
            {
                enemy.HitPoints -= damage;
                Console.WriteLine($"{enemy.Name} takes {damage} damage and has {enemy.HitPoints} HP remaining".PadRight(80));

                if (enemy.HitPoints <= 0)
                {
                    Console.Write($"{enemy.Name} has been killed!".PadRight(80));
                    enemy.EnemyKilled(enemy);
                }
            }
            else
            {
                Console.WriteLine($"The {enemy.Name} has blocked the damage!".PadRight(80));
            }
            
        }

    }
}
