public class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            GameManager.StartScreen.DisplayStartScreen();
            GameManager.StartGame();
            if (GameManager.levelNum > 10)
            {
                GameManager.VictoryScreen.DisplayVictoryScreen();
            }
            else GameManager.DeathScreen.DisplayDeathScreen();
        }
    }
}