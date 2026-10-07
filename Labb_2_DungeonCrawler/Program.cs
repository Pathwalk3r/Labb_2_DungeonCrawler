
using Labb_2_DungeonCrawler;

internal class Program
{
    private static void Main()
    {
        void main()
        {
            LevelData myLevel = new LevelData();
            myLevel.Load(Path.Combine(AppContext.BaseDirectory, "Levels\\Level1.txt"));
        }
        main();
    }


}

