public class MazeTile
{
    public sbyte X { get; set; }
    public sbyte Y { get; set; }
    public TileKind Kind { get; set; }
    public MazeTile[] Neighbors { get; set; }
    
    public bool Equals(MazeTile other)
    {
        return this.X == other.X && this.Y == other.Y;
    }
}
