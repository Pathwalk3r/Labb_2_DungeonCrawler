using System;
using System.Collections.Generic;
using System.Text;
using Labb_2_DungeonCrawler.Entities;
using Labb_2_DungeonCrawler.Entities.Enemies;

namespace Labb_2_DungeonCrawler
{
    class LevelData
    {
        private List<LevelEntity> _entity;

        public List<LevelEntity> Entity 
        { 
            get => _entity; 
        }

        public void Load(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            
            string line = sr.ReadLine();
            List<LevelEntity> myLevel = new List<LevelEntity>(); 

            int y = 0;

            while (line != null)
            {

                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x])
                    {
                        case '#':
                            myLevel.Add(new Wall(x, y));
                            continue;
                        case 'r':
                            myLevel.Add(new Rat(x, y));
                            continue;
                        case 's':
                            myLevel.Add(new Snake(x, y));
                            continue;
                        case '@':
                            myLevel.Add(new Player(x, y));
                            continue;
                    }
                }
                y++;
                line = sr.ReadLine();
            }
            sr.Close();

            foreach (var entity in myLevel)
            {
                entity.Draw();
            }
            
            
                
            
        }
    }
}
