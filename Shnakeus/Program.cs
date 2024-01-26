namespace Shnakeus;

public class Program
{
    public Program()
    {
        Game game = new();
        game.StartGame();
    }
    
    public static void Main(string[] args)
    {
        new Program();
    }    
}