public class RangedMiniBoss
{
    public Point Position;
    public int MaxHP;
    public int CurHP;
    public bool SpotsPlayer;

    public bool IsDead()
    {
        if (CurHP <= 0)
            return true;
        return false;
    }

    private Player Player { get { return GameManager.Player; } }

    public RangedMiniBoss(Point position)
    {
        Position = position;
        MaxHP = 10;
        CurHP = MaxHP;
        SpotsPlayer = false;
    }

    public void SpotAndShoot()
    {
        if (SpotsPlayer && Player.Position.X != Position.X && Player.Position.Y != Position.Y)
        {
            GameManager.GameLog.LogEvent("You are no longer in Mini-Boss's sight");
            SpotsPlayer = false;
        }
        if (SpotsPlayer)
        {
            Player.CurHP -= 7;
            GameManager.GameLog.LogEvent("You were hit for 7 damage, seemingly from out of nowhere");
        }
        if (!SpotsPlayer && Player.Position.X == Position.X || Player.Position.Y == Position.Y)
        {
            SpotsPlayer = true;
            GameManager.GameLog.LogEvent("Mini-Boss has spotted you");
        }
        else
        {
            SpotsPlayer = false;
        }
    }
}