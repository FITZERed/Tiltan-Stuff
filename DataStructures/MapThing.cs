public class MapThing
{
    char[,] map = new char[12, 12];
    public void CreateMap()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (i == 0 || i == 12 || j == 0 || j == 12)
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
                Console.WriteLine(map[i, j]);
            }
        }
    }
}