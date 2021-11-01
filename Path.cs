public class Path
{
    public HashSet<MazeTile> Tiles { get; set; }
    public bool Ended = false;
    public bool ReachedEnd = false;
    public int Length = 0;

    public Path(MazeTile tile)
    {
        Tiles = new HashSet<MazeTile>();
        Tiles.Add(tile);
    }
    public Path(Path path, MazeTile tile)
    {
        Tiles = new HashSet<MazeTile>(path.Tiles);
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
        Tiles.Add(tile);
    }

    public void End()
    {
        Ended = true;
        ReachedEnd = Tiles.Last().Kind == TileKind.Finish;
        Length = Tiles.Count;
    }
}
