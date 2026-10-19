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

            int yCord = 5;

            while (line != null)
            {

                for (int xCord = 0; xCord < line.Length; xCord++)
                {
                    switch (line[xCord])
                    {
                        case '#':
                            _entity.Add(new Wall(xCord, yCord));
                            continue;
                        case 'r':
                            _entity.Add(new Rat(xCord, yCord));
                            continue;
                        case 's':
                            _entity.Add(new Snake(xCord, yCord));
                            continue;
                        case '@':
                            _entity.Add(new Player(xCord, yCord));
                            continue;
                    }
                }
                yCord++;
                line = sr.ReadLine();
            }
            sr.Close();

            Player? player = (Player?)_entity.Find(entity => entity is Player);

            foreach (var entity in _entity)
            {
                entity.WithinVisionRange(player);
                entity.Draw();
            }
        }
        public void RemoveDeadEntities(Enemy enemy)
        {
            _entity.Remove(enemy);
        }
    }
}
