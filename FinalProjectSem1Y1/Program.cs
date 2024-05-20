public class Program
{
    static void Main(string[] args)
    {
        //GameManager.StartGame();
       //Make This a Function
        char[,] a = MapBuilder.ReadTextFile("Level 1.txt");
        for (int i = 0; i <= 11; i++) 
        {
            for (int j = 0; j <= 20; j++) 
            {
                Console.Write(a[i,j]);
            }
            Console.WriteLine();
        }
    }
}