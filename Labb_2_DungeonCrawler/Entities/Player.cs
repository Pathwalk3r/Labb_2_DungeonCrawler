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

        public Player(int xCord,  int yCord) : base(xCord, yCord)
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

            int newXCord = this.XCord;
            int newYCord = this.YCord;

            switch (input)
            {
                case ConsoleKey.LeftArrow:
                    newXCord--;
                    break;
                case ConsoleKey.RightArrow:
                    newXCord++;
                    break;
                case ConsoleKey.UpArrow:
                    newYCord--;
                    break;
                case ConsoleKey.DownArrow:
                    newYCord++;
                    break;
                case ConsoleKey.Escape:
                    gameover = true;
                    break;
            }

            if (!ValidMove(entities, newXCord, newYCord))
            {
                var target = entities.FirstOrDefault(e => e.XCord ==  newXCord && e.YCord == newYCord);
                if (target is Enemy enemy)
                {
                    int yPosForWriting = 2;
                    PlayerAttack(enemy, yPosForWriting);
                    if (enemy.IsAlive)
                    {
                        yPosForWriting = 3;
                        enemy.EnemyAttack(this, yPosForWriting);
                    }
                    else
                    {
                        yPosForWriting = 3;
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                    }
                }
                return false;
            }

            DrawMovement(_sprite, newXCord, newYCord);
            return gameover;
        }

        public void PlayerAttack(Enemy enemy, int yPosForWriting)
        {
            int playerAttack = this.AttackDice.Throw();
            int enemyDefense = enemy.DefenseDice.Throw();

            string summery = String.Empty;

            int damage = playerAttack - enemyDefense;

            summery += $"{this.Name} attacked with {AttackDice.ToString()} and deal {playerAttack} damage. ";
            

            if (damage > 0)
            {
                enemy.HitPoints -= damage;
                summery += $"{enemy.Name} takes {damage} damage and has {enemy.HitPoints} HP remaining. ";

                if (enemy.HitPoints <= 0)
                {
                    enemy.EnemyKilled(enemy);
                    summery += $"{enemy.Name} has been killed!";
                }
            }
            else
            {
                summery += $"The {enemy.Name} has blocked the damage!";
            }
            WriteAtLine(yPosForWriting, summery);
            Thread.Sleep(200);
        }

    }
}
