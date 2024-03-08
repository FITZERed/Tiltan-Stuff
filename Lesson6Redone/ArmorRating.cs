// ---- C# I (Dor Ben Dor) ----
//        Daniel Fitzer
// ----------------------------


//Exercise C - Armor Class
namespace myNamespace
{
    public class ArmorRating
    {
        public float Evasion { get; set; }
        public int Shield { get; set; }
        public ArmorRating(int shield, float evasion)
        {
            Evasion = evasion;
            Shield = shield;
        }
    }
}