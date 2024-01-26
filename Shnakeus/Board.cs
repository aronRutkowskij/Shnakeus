namespace Shnakeus;

public class Board
{
    public int w, h;
    public Shnakey _shnakey;
    public Item _bait;
    public List<Item> _obstacles;
    private string[,] board;
    
    public Board(Shnakey shnakey, int width, int height)
    {
        _shnakey = shnakey;
        w = width;
        h = height;
    }

    public void SetBoard()
    {
        board = new string[h,w];
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if (i == 0 || i == h-1)
                {
                    board[i, j] = "#";
                }
                else if (j == 0 || j == w-1)
                {
                    board[i, j] = "#";
                }
                else
                {
                    board[i, j] = " ";
                }
    
                foreach (var pos in _shnakey.Positions)
                {
                    if (pos.X == j && pos.Y == i)
                    {
                        board[i, j] = "*";
                    }
                }
                if(_obstacles != null)
                {
                    foreach (var o in _obstacles)
                    {
                        if (o.Position.X == j && o.Position.Y == i)
                        {
                            board[i, j] = "#";
                        }
                    } 
                }

                if (_bait != null)
                {
                    if (_bait.Position.X == j && _bait.Position.Y == i)
                    {
                        board[i, j] = "*";
                    }
                }
            }
        }
    }
    
    public void DrawBoard()
    {
        SetBoard();
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                Console.Write(board[i,j]);
            }
            Console.WriteLine("");
        } 
    }

    public string SetGameOverScreen()
    {
        string m1 = "GAME OVER";
        string m2 = "N => NEUSTART";
        string m3 = "B => BEENDEN";
        int cx1 = (w / 2) - (m1.Length / 2);
        int cx2 = (w / 2) - (m2.Length / 2);
        int cx3 = (w / 2) - (m3.Length / 2);

        for (int i = 0; i < m1.Length; i++)
        {
            board[h / 2 - 2, cx1 + i] = m1[i].ToString();
        }
        for (int i = 0; i < m2.Length; i++)
        {
            board[h / 2 - 1, cx2 + i] = m2[i].ToString();
        }
        for (int i = 0; i < m3.Length; i++)
        {
            board[h / 2, cx3 + i] = m3[i].ToString();
        }
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                Console.Write(board[i,j]);
            }
            Console.WriteLine("");
        } 
        string choice = Console.ReadLine();
        return choice;
    }
}