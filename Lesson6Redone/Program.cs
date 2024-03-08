// ---- C# I (Dor Ben Dor) ----
//        Daniel Fitzer
// ----------------------------


//Program - Part for each exercise is titled and closed in its own comment
//          Remove the appropriate /**/ from the desired part to check
namespace myNamespace
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Exercise A
            /* 
            Printer p = new Printer("I am now become Printer, the writer of memes.", 22);
            p.Print(OverflowBehavior.Clip);
            p.Print(OverflowBehavior.Overflow);
            p.Print(OverflowBehavior.Ellipsis);
            */



            //Exercise C
            /* 
            Unit player = new Unit("Player", 15, 4, 0.25f, 5, 0.8f);
            int enemyBaseMaxHP = 6;
            int enemyBaseShield = 1;
            float enemyBaseEvasion = 0.1f;                  //base enemy stats
            int enemyBaseDamage = 3;
            float enemyBaseHitChance = 0.6f;
            while (!player.IsDead)             //game loop, goes as long as player is alive
            {
                Console.WriteLine("A New Challenger Approaches.");
                Unit enemy = new Unit("Enemy", enemyBaseMaxHP, enemyBaseShield, enemyBaseEvasion, enemyBaseDamage, enemyBaseHitChance);
                while (!enemy.IsDead)                      //while enemy is alive, loops combat
                {
                    Console.WriteLine("Choose an action:");
                    Console.WriteLine("1. Attack");
                    Console.WriteLine("2. Heal");
                    if (int.TryParse(Console.ReadLine(), out var input))
                    {
                        switch (input)
                        {
                            case 1:
                                player.TryAttack(enemy);
                                break;
                            case 2:
                                player.Heal(3);
                                break;
                            default:
                                Console.WriteLine("wrong input");
                                continue;
                        }
                    }
                    if (enemy.IsDead) { break; }
                    enemy.TryAttack(player);
                    if (player.IsDead) { break; }
                }
                if (player.IsDead) { Console.WriteLine("Player was defeated, Game Over."); break; } //when enemy is dead, checks if player is alive, then goes to upgrade
                int inputUpgrade = 0;
                while (inputUpgrade != 1 && inputUpgrade != 2)          //loops so players have to take a vaild option
                {
                    Console.WriteLine("Choose upgrade:");
                    Console.WriteLine("1. Armor Upgrade");
                    Console.WriteLine("2. Weapon Upgrade");
                    if (int.TryParse(Console.ReadLine(), out inputUpgrade))
                    {
                        switch (inputUpgrade)
                        {
                            case 1:
                                player.UpgradeShield();
                                player.UpgradeShield();
                                Console.WriteLine("Shield rating upgraded by 2.");
                                break;
                            case 2:
                                player.UpgradeDamage();
                                player.UpgradeDamage();
                                Console.WriteLine("Weapon damage upgraded by 2.");
                                Console.WriteLine();
                                break;
                            default:
                                Console.WriteLine("Invalid input, try again.");
                                Console.WriteLine();
                                continue;
                        }
                    }
                }
                enemyBaseMaxHP++;
                enemyBaseShield++;
                enemyBaseEvasion += 0.05f;               //enemy escalation
                enemyBaseDamage++;
                enemyBaseHitChance += 0.05f;
            }*/
        }
    }
}
