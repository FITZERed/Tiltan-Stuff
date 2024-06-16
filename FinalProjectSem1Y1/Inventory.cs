public class Inventory
{
    public List<Weapon> ObtainedWeapons = new List<Weapon>();
    public int HealingPotions { get; private set; } = 0;

    public void AddPotion()
    {
        HealingPotions++;
    }
    public void SubtractPotion()
    {
        HealingPotions--;
    }
    public Inventory()
    {
        ObtainedWeapons.Add(new Weapon(WeaponType.Spear));
    }
}