public class Path
{
    public List<MazeTile> Tiles { get; set; } = new List<MazeTile>();
    public bool Ended = false;
    public bool ReachedEnd = false;
    public int Length = 0;

    public Path()
    {

    }
    public Path(Path path, MazeTile tile)
    {
        Tiles.AddRange(path.Tiles);
        Tiles.Add(tile);
    }

    public MazeTile CurrentTile()
    {
        return Tiles.Last();
    }

    public bool UnpassedTile(MazeTile tile)
    {
        return !Tiles.Contains(tile);
    }

    public void AddTile(MazeTile tile)
    {
        if (Tiles.Contains<MazeTile>(tile))
        {
            throw new ArgumentException("TILE ALREADY PASSED");
        }
        Tiles.Add(tile);
    }

    public void End()
    {
        Ended = true;
        ReachedEnd = Tiles.Last().Kind == TileKind.Finish;
        Length = Tiles.Count;
    }
}
