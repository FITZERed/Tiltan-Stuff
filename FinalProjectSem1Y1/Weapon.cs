public class Weapon
{
    public WeaponType WeaponType;
    public int Power;
    public AttackAOE AttackAOE;
    public string WeaponName;

    public Weapon(WeaponType weaponType)
    {
        WeaponType = weaponType;
        switch(weaponType)
        {
            case WeaponType.Spear:
                Power = 2;
                AttackAOE = AttackAOE.TwoForward;
                WeaponName = "Spear";
                    break;
            case WeaponType.Axe:
                Power = 3;
                AttackAOE = AttackAOE.ThreeInFront;
                WeaponName = "Axe";
                break;
            case WeaponType.ShortBow:
                Power = 2;
                AttackAOE = AttackAOE.Ranged;
                WeaponName = "Shortbow";
                break;
            case WeaponType.LegendarySword:
                Power = 5;
                AttackAOE = AttackAOE.PlusShape;
                WeaponName = "Firelight";
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