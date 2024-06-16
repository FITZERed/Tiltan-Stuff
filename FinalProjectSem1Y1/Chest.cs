public class Chest
{
    public Point Position;
    public ChestContent Content;
    public bool IsLooted { get; private set; } = false;

    public void LootChest()
    {
        IsLooted = true;
    }
    public Chest(Point position, ChestContent chestContent)
    {
        Position = position;
        Content = chestContent;
    }
}

public enum ChestContent
{
    HealingPotion,
    Axe,
    LegendSword
}