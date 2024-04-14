public class Map
{
    char[,] map = new char[12, 12];
    public Map()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (i == 0 || i == map.GetLength(0) - 1 || j == 0 || j == map.GetLength(1) - 1)
                {
                    map[i, j] = 'E';
                }
                else
                {
                    map[i, j] = ' ';
                }
            }
        }
    }

    public void PrintMap()
    {
        for (int i = 0; i < 12;i++)
        {
            for (int j = 0;j < 12; j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine();
        }
    }

    public enum TileENUM
    {
        Empty,
        Wall,
        Player,
        Enemy
    }
}