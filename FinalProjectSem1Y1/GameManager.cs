public static class GameManager
{
    public static Level CurrentLevel;
    public static void StartGame()
    {
        int levelNum = 1;
        CurrentLevel = new Level(levelNum);
        while (!CurrentLevel.Player.IsDead())
        {
            CurrentLevel.Player.PlayerInput();
            //here account for more things player input may cause
            //then put environmental changes
            CurrentLevel.RefreshMap();
            CurrentLevel.PrintCurrentMapState();
        }
    }
}