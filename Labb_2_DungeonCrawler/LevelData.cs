using System;
using System.Collections.Generic;
using System.Text;
using Labb_2_DungeonCrawler.Entities;
using Labb_2_DungeonCrawler.Entities.Enemies;

namespace Labb_2_DungeonCrawler
{
    class LevelData
    {
        private List<LevelEntity> _entity = new List<LevelEntity>();

        public List<LevelEntity> Entity 
        { 
            get => _entity; 
        }

        public void Load(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            
            string line = sr.ReadLine();

            int y = 5;

            while (line != null)
            {

                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x])
                    {
                        case '#':
                            _entity.Add(new Wall(x, y));
                            continue;
                        case 'r':
                            _entity.Add(new Rat(x, y));
                            continue;
                        case 's':
                            _entity.Add(new Snake(x, y));
                            continue;
                        case '@':
                            _entity.Add(new Player(x, y));
                            continue;
                    }
                }
                y++;
                line = sr.ReadLine();
            }
            sr.Close();

            Player? player = (Player?)_entity.Find(x => x is Player);

            //Console.WriteLine("Enter your name:");
            //player?.Name = Console.ReadLine();
            //Console.Clear();

            //Console.Write($"{player.Name} HP [{player?.HitPoints}/{player?._maxHp}]".PadRight(30));
            //Console.Write($"Turn: 0");

            int playerPosX = player.X;
            int playerPosY = player.X;

            foreach (var entity in _entity)
            {
                entity.WithinVisionRange(playerPosX, playerPosY);
                entity.Draw();
            }
        }
        public void RemoveDeadEntities(Enemy enemy)
        {
            _entity.Remove(enemy);
        }
    }
}
