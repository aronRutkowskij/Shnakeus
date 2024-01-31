namespace Shnakeus;

public class Game
{
    private Board board;
    private Shnakey shnakey;
    public List<Item> Obstacles = new List<Item>();
    public Item Bait = new Item();
    public int gamemode;
    private int w, h;
    
    public Game()
    {
        shnakey = new Shnakey
        {
            L = 5,
            Direction = 8,
            IsAlive = true,
            Positions = new List<Positions>()
        };
        
        ShowDialogue();
        
        shnakey.SetPositions(w / 2, h / 2); 
        
        board = new Board(shnakey, w, h);

        if (gamemode == 1)
        {
            Bait = null;
            Obstacles = null;
        }
        else if (gamemode == 2)
        {
            Bait = null;
            Obstacles = SetObstacle();
        }
        else if (gamemode == 3)
        {
            Bait = SetBait();
            Obstacles = null;
        }
        else if (gamemode == 4)
        {
            Bait = SetBait();
            Obstacles = SetObstacle();
        }
        
        board.SetBoard();
    }
    
    public void GameLoop()
    {
        while (shnakey.IsAlive)
        {
            Console.Clear();
            if (Console.KeyAvailable)
            {
                CheckDirectionChange();
            }
            SetNewPositions(); 
            CheckItemCollision();
            board._shnakey = shnakey;
            board.DrawBoard();
            Thread.Sleep(500);
        }
        string choice = board.SetGameOverScreen();
        if (choice.Equals("N") || choice.Equals("n"))
        {
            RestartGame();
        }
        else
        {
            Environment.Exit(0);
        }
    }

    public void ShowDialogue()
    {
        Console.Write("Gebe die Höhe des Spielfeldes an: ");
        h = Convert.ToInt32(Console.ReadLine());
        Console.Write("Gebe die Breite des Spielfeldes an: ");
        w = Convert.ToInt32(Console.ReadLine()) * 2;
      
        Console.WriteLine("* * * * *   W E L C O M E   T O   S N A K E   * * * * *");
        Console.WriteLine("Wähle einen Spielmodus: ");
        Console.WriteLine("1: Einfacher Modus.");
        Console.WriteLine("2: Erweiterter Modus mit Hindernissen.");
        Console.WriteLine("3: Erweiterter Modus mit Bait.");
        Console.WriteLine("4: Erweiterter Modus mit Hindernissen und Bait.");

        gamemode = Convert.ToInt32(Console.ReadLine());
    }
    
    public void StartGame()
    {
        board.DrawBoard();
        // added this part
        Console.WriteLine("Drücken Sie eine beliebige Taste, um das Spiel zu beginnen");
        // wait for the user to press any key
        Console.ReadKey();
        GameLoop();
    }
    
    public void RestartGame()
    {
        shnakey = new Shnakey
        {
            L = 5,
            Direction = 8,
            IsAlive = true,
            Positions = new List<Positions>()
        };
        ShowDialogue();
       
        shnakey.SetPositions(w / 2, h / 2);   
        if (gamemode == 1)
        {
            Bait = null;
            Obstacles = null;
        }
        else if (gamemode == 2)
        {
            Bait = null;
            Obstacles = SetObstacle();
        }
        else if (gamemode == 3)
        {
            Bait = SetBait();
            Obstacles = null;
        }
        else if (gamemode == 4)
        {
            Bait = SetBait();
            Obstacles = SetObstacle();
        }
        
        Console.Clear();
        board.SetBoard();
        GameLoop();
    }

    public void CheckDirectionChange()
    {
        ConsoleKeyInfo key = Console.ReadKey(true); // true prevents the key from being displayed

        if (key.Key == ConsoleKey.UpArrow)
        {
            if (shnakey.Direction != 2)
            {
                shnakey.Direction = 8;
            }
        }
        else if (key.Key == ConsoleKey.DownArrow)
        {
            if (shnakey.Direction != 8)
            {
                shnakey.Direction = 2;
            }
        }
        else if (key.Key == ConsoleKey.LeftArrow)
        {
            if (shnakey.Direction != 6)
            {
                shnakey.Direction = 4; 
            }
        }
        else if (key.Key == ConsoleKey.RightArrow)
        {
            if (shnakey.Direction != 4)
            {
                shnakey.Direction = 6;
            }
        }
    }
    
    public void SetNewPositions()
    {
        Positions newHead = new Positions(shnakey.Positions.Last().X, shnakey.Positions.Last().Y);

        switch (shnakey.Direction)
        {
            case 8:
                newHead.Y -= 1;
                break;
            case 2:
                newHead.Y += 1;
                break;
            case 4:
                newHead.X -= 1;
                break;
            case 6:
                newHead.X += 1;
                break;
            default:
                break;
        }
        
        if (newHead.X <= 0 || newHead.X >= board.w - 1 || newHead.Y <= 0 || newHead.Y >= board.h - 1)
        {
            shnakey.IsAlive = false;
            return;
        }
        
        shnakey.Positions.Add(newHead);

        if (shnakey.Positions.Count > shnakey.L)
        {
            shnakey.Positions.RemoveAt(0);
        }

    }

    public Item SetBait()
    {
        Random r = new();
        
        Item bait = new Item
        {
            Name = "Bait",
            Symbol = "+",
            Position = new Positions(r.Next(1, board.w), r.Next(1, board.h))
        };

        board._bait = bait;
        return bait;
    }

    public List<Item> SetObstacle()
    {
        List<Item> obstacles = new List<Item>();
        Random r = new();

        int numOfObstacles = r.Next(1, 5);

        for (int n = 0; n < numOfObstacles; n++)
        {
            
            int obstacleSize = r.Next(1, 4);

            if (obstacleSize == 1)
            {
                obstacles.Add(new Item
                {
                    Name = "Obstacle",
                    Symbol = "#",
                    Position = new Positions(r.Next(1, board.w), r.Next(1, board.h))
                });
            }
            else if (obstacleSize >= 2)
            {
                int x = r.Next(1, board.w);
                int y = r.Next(1, board.h);

                for (int i = 0; i < obstacleSize; i++)
                {
                    for (int j = 0; j < obstacleSize; j++)
                    {
                        obstacles.Add(new Item
                        {
                            Name = "Obstacle",
                            Symbol = "#",
                            Position = new Positions(x + i, y + j)
                        });
                    }
                }
            }
        }

        board._obstacles = obstacles;
        return obstacles;
    }
    
    public void CheckItemCollision()
    {
        Positions head = new Positions(shnakey.Positions.Last().X, shnakey.Positions.Last().Y);

        // check obstacle collision
        if (Obstacles != null)
        {
            foreach (var o in Obstacles)
            {
                if (o.Position.X == head.X && o.Position.Y == head.Y)
                {
                    shnakey.IsAlive = false;
                }
            } 
        }
        
        // check bait collision
        if (Bait != null)
        {
            if (Bait.Position.X == head.X && Bait.Position.Y == head.Y)
            {
                shnakey.L++;
                Random r = new();
                Bait.Position = new Positions(r.Next(1, board.w), r.Next(1, board.h));
                Console.WriteLine("You've cached bait");
            } 
        }
    }
}

