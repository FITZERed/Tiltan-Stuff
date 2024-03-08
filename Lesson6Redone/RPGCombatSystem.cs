// ---- C# I (Dor Ben Dor) ----
//        Daniel Fitzer
// ----------------------------


//Exercise C - Unit Class
namespace myNamespace
{
    public class Unit
    {
        private string _name;
        private int _maxHP;
        private int _currentHP;
        private Weapon _mainWeapon;
        private ArmorRating _armorRating;
        private Random _random;

        public bool IsDead { get { return _currentHP <= 0; } }

        public Unit(string name, int maxHP, int shield, float evasion, int damage, float hitChance)
        {
            _name = name;
            if(maxHP <= 0) maxHP = 0;
            this._maxHP = maxHP;
            _currentHP = _maxHP;
            _mainWeapon = new Weapon(damage, hitChance);
            _armorRating = new ArmorRating(shield, evasion);
            _random = new Random();
            if(evasion >= 0.9f) { evasion = 0.9f; }
            if(hitChance >= 0.9f) {  hitChance = 0.9f; }
            
        }
        public bool TryAttack(Unit defendingUnit)
        {
            double randomNumber = _random.NextDouble();
            if (_mainWeapon.HitChance > randomNumber)
            {
                Console.WriteLine($"{_name} attacked {defendingUnit._name}");
                defendingUnit.RecieveAttack(_mainWeapon);
                return true;
            }
            Console.WriteLine($"{_name} missed {defendingUnit._name}");
            Console.WriteLine();//space for readability of program
            return false;
        }
        void RecieveAttack(Weapon weapon)
        {
            double randomNumber = _random.NextDouble();
            if (_armorRating.Evasion > randomNumber)
            {
                Console.WriteLine("Attack dodged!");
                Console.WriteLine();//space for readability of program
            }
            else
            {
                int damageDealt = weapon.Damage - _armorRating.Shield;
                if (damageDealt > 0)
                {
                    _currentHP -= damageDealt;
                    if( _currentHP < 0 ) _currentHP = 0;
                    Console.WriteLine($"{_name} recieved {damageDealt} damage!");
                }
                else { Console.WriteLine("It dealt no damage..."); }
                Console.WriteLine($"{_name} has {_currentHP} HP left.");
                Console.WriteLine();//space for readability of program
            }

        }
        public void Heal(int value)
        {
            if (value <= 0) return;
            _currentHP += value;

            if (_currentHP > _maxHP)
                _currentHP = _maxHP;
            Console.WriteLine($"{_name} healed {value} HP.");
            Console.WriteLine($"{_name} has {_currentHP} HP remaining.");
            Console.WriteLine();//space for readability of program
        }
        public void UpgradeShield()
        {
            _armorRating.Shield++;
        }
        public void UpgradeDamage()
        {
            _mainWeapon.Damage++;
        }

    }
}