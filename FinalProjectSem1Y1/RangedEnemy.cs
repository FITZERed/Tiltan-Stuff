public class RangedEnemy
{
    public Point Position;
    public int MaxHP;
    public int CurHP;
    public int Damage;
    private RangedEnemyState State;
    public FaceDirection Direction;
    private Player Player { get { return GameManager.Player; } }
    
    public bool IsDead()
    {
        if (CurHP <= 0)
            return true;
        return false;
    }
    public RangedEnemy(Point position, FaceDirection direction)
    {
        Position = position;
        MaxHP = 3;
        CurHP = MaxHP;
        Damage = 2;
        State = RangedEnemyState.PrepingShot;
        Direction = direction;
    }
    public void CycleShooting()
    {
        if (IsDead()) return;
        if (State == RangedEnemyState.PrepingShot)
        {
            State = RangedEnemyState.DrawingShot;
        }
        else if(State == RangedEnemyState.DrawingShot)
        {
            EnemyShoot(Direction);
            State = RangedEnemyState.JustShot;
        }
        else if (State == RangedEnemyState.JustShot)
        {
            State= RangedEnemyState.PrepingShot;
        }
    }
    public void EnemyShoot(FaceDirection direction)
    {
        switch (direction)
        {
            case FaceDirection.Left:
                for (int i = Position.X - 1; i > 0; i--)
                {
                    if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Empty)
                        continue;
                    else if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Wall)
                        break;
                    else if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Player) 
                    {
                        Player.CurHP -= Damage;
                        break;
                    }
                    else { break; }
                    //else if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.StandardEnemy)
                    //{
                           //COOL IDEA, LOOK INTO IT MAYBE LATER
                    //}
                }
                break;
            case FaceDirection.Right:
                for (int i = Position.X + 1; i < 24; i++)
                {
                    if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Empty)
                        continue;
                    else if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Wall)
                        break;
                    else if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Player)
                    {
                        Player.CurHP -= Damage;
                        break;
                    }
                    else { break; }
                }
                    break;
            case FaceDirection.Up:
                for (int i = Position.Y - 1; i > 0; i--)
                {
                    if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Empty)
                        continue;
                    else if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Wall)
                        break;
                    else if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Player)
                    {
                        Player.CurHP -= Damage;
                        break;
                    }
                    else { break; }
                }
                    break;
            case FaceDirection.Down:
                for (int i = Position.Y + 1; i < 24; i++)
                {
                    if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Empty)
                        continue;
                    else if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Wall)
                        break;
                    else if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Player)
                    {
                        Player.CurHP -= Damage;
                        break;
                    }
                    else { break; }
                }
                break;
            default:
                break;
        }
    }
}

public enum RangedEnemyState
{
    PrepingShot,
    DrawingShot,
    JustShot
}
public enum FaceDirection
{
    Up,
    Down,
    Left,
    Right,
    None
}