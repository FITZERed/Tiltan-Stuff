public class Weapon
{
    public WeaponType WeaponType;
    public int Power;
    public AttackAOE AttackAOE;

    public Weapon(WeaponType weaponType)
    {
        switch(weaponType)
        {
            case WeaponType.Spear:
                Power = 2;
                AttackAOE = AttackAOE.TwoForward;
                    break;
            case WeaponType.Axe:
                Power = 3;
                AttackAOE = AttackAOE.ThreeInFront;
                break;
            case WeaponType.ShortBow:
                Power = 1;
                AttackAOE = AttackAOE.Ranged;
                break;
            case WeaponType.LegendarySword:
                Power = 5;
                AttackAOE = AttackAOE.PlusShape;
                break;
            default: throw new ArgumentException("Weapon Not Found");
        }
    }
}

public enum AttackAOE
{
    TwoForward,
    ThreeInFront,
    Ranged,
    PlusShape,
    OneForward
}

public enum WeaponType
{
    Spear,
    Axe,
    ShortBow,
    LegendarySword,
    Dagger
}