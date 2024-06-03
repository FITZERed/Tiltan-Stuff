public class Inventory
{
    public List<Weapon> ObtainedWeapons = new List<Weapon>();
    public int HealingPotions;

    public Inventory()
    {
        ObtainedWeapons.Add(new Weapon(WeaponType.Spear));
    }
}