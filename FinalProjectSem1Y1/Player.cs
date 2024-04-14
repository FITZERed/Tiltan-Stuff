public class Player
{
    public Point Position { get; set; }
    public int MaxHP;
    public int CurHP;
    public bool IsDead()
    {
        if (CurHP <= 0) return true;
        else return false;
    }


    public Player(Point position)
    {
        Position = position;
        MaxHP = 50;
        CurHP = MaxHP;
    }

    public void PlayerInput()
    {
        var input = Console.ReadKey();
        switch (input.Key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                Position.Y--;
                if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X] != Level.TileENUM.Empty) Position.Y++;
                //need to check if new tile is valid, not sure how to access CurrentMapState from player...
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                Position.Y++;
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                Position.X++;
                break;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                Position.X--;
                break;
            default:
                break;
        }
    }




}