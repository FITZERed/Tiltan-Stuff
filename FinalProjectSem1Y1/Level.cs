using System.Security.Cryptography.X509Certificates;

public class Level
{
    public TileENUM[,] map = new TileENUM[12, 22];
    public Point PlayerStartingPosition;
    public TileENUM[,] CurrentMapState;
    public Player Player;
    
    public Level(int levelNum)
    {
        Player = new Player(PlayerStartingPosition);
        SetStartingMapState(levelNum);
        CurrentMapState = map;
        PrintCurrentMapState();
    }

    public void SetStartingMapState(int levelNum)
    {
        switch (levelNum)
        {
            case 1:
                PlayerStartingPosition = new Point(5, 5);
                Player.Position = PlayerStartingPosition;
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (i == 0 || i == map.GetLength(0) - 1 || j == 0 || j == map.GetLength(1) - 1)
                        {
                            map[i, j] = TileENUM.Wall;
                        }
                        else if (i == 3 && (j <= 6 || (j >= 10 && j <= 16))) map[i, j] = TileENUM.Wall;
                        else if (j == 16 && (i == 1 || i == 2 || i ==7)) map[i, j] = TileENUM.Wall;
                        else if (i == 6 && (j <= 10 || j == 14)) map[i, j] = TileENUM.Wall;
                        else if (i == 7 && (j == 10 || j >= 14)) map[i, j] = TileENUM.Wall;
                        else if (i == PlayerStartingPosition.Y && j == PlayerStartingPosition.X)
                        {
                            map[i, j] = TileENUM.Player;
                        }
                        else
                        {
                            map[i, j] = TileENUM.Empty;
                        }
                    }
                }
            break;

        }
    }
    public static char ConvertTileENUMtoChar(TileENUM tile)
    {
        switch (tile)
        {
            case TileENUM.Empty:
                return ' ';
            case TileENUM.Wall:
                return '█';
            case TileENUM.Player:
                return 'P';
            case TileENUM.StandardEnemy:
                return 'E';
            default:
                throw new ArgumentOutOfRangeException(nameof(tile));
        }
    }
    //♦
    //♠

    public void PrintCurrentMapState()
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < CurrentMapState.GetLength(0); i++)
        {
            for (int j = 0; j < CurrentMapState.GetLength(1); j++)
            {
                char tileToPrint = ConvertTileENUMtoChar(CurrentMapState[i, j]);
                Console.Write(tileToPrint);
            }
            Console.WriteLine();
        }
    }
    public void RefreshMap()
    {
        for (int i = 0; i < CurrentMapState.GetLength(0); ++i)
        {
            for (int j = 0; j < CurrentMapState.GetLength(1); ++j)
            {
                if (CurrentMapState[i, j] == TileENUM.Wall) CurrentMapState[i, j] = TileENUM.Wall;
                else if (Player.Position.Y == i && Player.Position.X == j) 
                    CurrentMapState[i, j] = TileENUM.Player;
                else CurrentMapState[i, j] = TileENUM.Empty;
            }
        }

        //need to get the player position and things, maybe this shoud be in GameManager
    }
    public enum TileENUM
    {
        Empty,
        Wall,
        Player,
        StandardEnemy
    }
}