public class MazeTile
{
    public int X { get; set; }
    public int Y { get; set; }
    public TileKind Kind { get; set; }
    public bool IsUsed { get; set; }

    public bool Equals(MazeTile other)
    {
        return this.X == other.X && this.Y == other.Y;
    }
}
