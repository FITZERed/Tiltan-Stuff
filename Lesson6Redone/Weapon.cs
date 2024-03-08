// ---- C# I (Dor Ben Dor) ----
//        Daniel Fitzer
// ----------------------------


//Exercise C - Weapon Class
namespace myNamespace
{
    public class Weapon
    {
        public int Damage { get; set; }
        public float HitChance { get; set; }
        public Weapon(int damage, float hitChance)
        {
            Damage = damage;
            HitChance = hitChance;
        }

    }
}