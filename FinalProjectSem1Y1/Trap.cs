public class Trap
{
    public Point Position;
    public TrapState State;
    public int Damage;
    private Player Player { get { return GameManager.Player; } }

    public Trap(Point position)
    {
        Position = position;
        State = TrapState.Hidden;
        Damage = 5;
    }
    public bool IsVisible()
    {
        if (State == TrapState.Hidden) return false;
        else return true;
    }
    public void DetectTrap()
    {
        if (State == TrapState.Hidden)
        {
            State = TrapState.Visible;
            GameManager.GameLog.LogEvent("You detect a trap");
        }
    }
    public void TriggerTrap()
    {
        if (State != TrapState.Triggered)
        {
            State = TrapState.Triggered;
            Player.CurHP -= Damage;
            GameManager.GameLog.LogEvent($"You stepped in a trap and take {Damage} damage");
        }
    }
}
public enum TrapState
{
    Hidden,
    Visible,
    Triggered
}