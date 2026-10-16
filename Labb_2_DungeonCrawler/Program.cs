using Labb_2_DungeonCrawler;
using Labb_2_DungeonCrawler.Entities;
using Labb_2_DungeonCrawler.Entities.Enemies;
using System.Linq.Expressions;

internal class Program
{
    private static void Main()
    {
        void main()
        {
            Console.CursorVisible = false;
            LevelData myLevel = new LevelData();
            myLevel.Load(Path.Combine(AppContext.BaseDirectory, "Levels\\Level1.txt"));

            Player? player = (Player?)myLevel.Entity.Find(player => player is Player);

            int x = player.XCord;
            int y = player.YCord;

            bool gameover = false;

            int turnCount = 0;

            while (!gameover)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey().Key;
                    gameover = player.update(input, myLevel.Entity);
                    
                    foreach (var element in myLevel.Entity.ToList())
                    {
                        
                        element.WithinVisionRange(player.XCord, player.YCord);
                        if (element is Wall wall)
                        {
                            wall.HasBeenDiscovered();
                            wall.Draw();
                        }
                        if (element is Enemy enemy)
                        {
                            if (!enemy.IsAlive)
                            {
                                myLevel.RemoveDeadEntities(enemy);

                            }
                            else
                            {
                                enemy.Update(myLevel.Entity);
                            }
                        }
                    }
                turnCount++;
                }
                Console.SetCursorPosition(0, 0);
                Console.Write($"{player.Name} HP [{player?.HitPoints}/{player?._maxHp}]".PadRight(30));
                Console.Write($"Turn: {turnCount}");
                if (player.HitPoints <= 0)
                {
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("You have died!".PadRight(120));
                    gameover = true;
                }
             Console.SetCursorPosition(0, 24); 
            }

        }
        main();
    }


}

