using System.Security.Cryptography.X509Certificates;

public class Level
{
    public TileENUM[,] map = new TileENUM[12, 21];
    public Point PlayerStartingPosition;
    public TileENUM[,] CurrentMapState;
    public Player Player;
    public EnemyLists EnemyLists = new();
    public Exit Exit;
    public Entrance Entrance;
    
    public Level(int levelNum)
    {
        Player = GameManager.Player;
        map = BuildInitialMapState(MapBuilder.ReadTextFile("Level " + levelNum + ".txt"));
        CurrentMapState = map;
        PrintCurrentMapState();
    }

    public TileENUM[,] BuildInitialMapState(char[,] charMatrix)
    {
        TileENUM[,] map = new TileENUM[charMatrix.GetLength(0), charMatrix.GetLength(1)];
        for (int i = 0; i < charMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < charMatrix.GetLength(1); j++)
            {
                switch (charMatrix[i, j])
                {
                    case ' ':
                        map[i, j] = TileENUM.Empty; break;
                    case 'P':
                        map[i, j] = TileENUM.Player;
                        PlayerStartingPosition = new Point(j, i);
                        Player.Position = PlayerStartingPosition;
                        Entrance = new Entrance(PlayerStartingPosition);
                            break;
                    case 'E':
                        map[i, j] = TileENUM.StandardEnemy;
                        EnemyLists.StandardEnemiesPresent.Add(new StandardEnemy(new Point(j, i)));
                            break;
                    case '█':
                        map[i, j] = TileENUM.Wall; break;
                    case 'X':
                        map[i, j] = TileENUM.Exit;
                        Exit = new Exit(new Point(j, i));
                        break;
                    default: throw new ArgumentOutOfRangeException(charMatrix[i, j].ToString());


                }
            }
        }

        return map;
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
                return '♦';
            case TileENUM.StandardEnemy:
                return '♠';
            case TileENUM.Exit:
                return 'X';
            case TileENUM.Entrance:
                return 'E';
            default:
                throw new ArgumentOutOfRangeException(nameof(tile));
        }
    }
    //♦
    //♠

    public void PrintCurrentMapState()
    {
        for (int i = 0; i < CurrentMapState.GetLength(0); i++)
        {
            Console.SetCursorPosition(0, i);
            for (int j = 0; j < CurrentMapState.GetLength(1); j++)
            {
                char tileToPrint = ConvertTileENUMtoChar(CurrentMapState[i, j]);
                Console.Write(tileToPrint);
            }
        }
    }
    public void RefreshMap()
    {
        
        for (int i = 0; i < CurrentMapState.GetLength(0); ++i)
        {
            for (int j = 0; j < CurrentMapState.GetLength(1); ++j)
            {
                if (CurrentMapState[i, j] == TileENUM.Wall) CurrentMapState[i, j] = TileENUM.Wall;
                else if (CurrentMapState[i, j] == TileENUM.Exit) CurrentMapState[i, j] = TileENUM.Exit;
                else if (i == PlayerStartingPosition.Y && j == PlayerStartingPosition.X) CurrentMapState[i, j] = TileENUM.Entrance;
                else CurrentMapState[i, j] = TileENUM.Empty;

            }
        }
        foreach (StandardEnemy standardEnemy in EnemyLists.StandardEnemiesPresent)
        {
            if (standardEnemy.IsDead()) continue;
            CurrentMapState[standardEnemy.Position.Y, standardEnemy.Position.X] = TileENUM.StandardEnemy;
        }
        CurrentMapState[Player.Position.Y, Player.Position.X] = TileENUM.Player;
        //need to get the player position and things, maybe this shoud be in GameManager
    }
   
    
}
public enum TileENUM
{
    Empty,
    Wall,
    Player,
    StandardEnemy,
    Exit,
    Entrance
}