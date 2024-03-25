namespace Shnakeus;

public class Shnakey
{
    public int L { get; set; }
    public int Direction { get; set; }
    public bool IsAlive { get; set; }
    public List<Positions>? Positions { get; set; }
    public void SetPositions(int startX, int startY)
    {
        Positions.Add(new Positions(startX, startY+2));
        Positions.Add(new Positions(startX, startY+1));
        Positions.Add(new Positions(startX, startY));
        Positions.Add(new Positions(startX, startY-1));
        Positions.Add(new Positions(startX, startY-2));
    }
}