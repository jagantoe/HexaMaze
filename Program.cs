var level = Levels.Level4;
var relevantTiles = FilterRelevantTiles(level);
SetNeighborTiles(relevantTiles);
var startTile = relevantTiles.Single(t => t.Kind == TileKind.Start);
var paths = new List<Path>() { new Path(startTile) };
var finishedPaths = new List<Path>();
do
{
    var path = paths[0];
    var possibleMoves = GetPossibleMoves(path);
    while (possibleMoves.Count == 1)
    {
        path.AddTile(possibleMoves.First());
        possibleMoves = GetPossibleMoves(path);
    }
    if (possibleMoves.Count == 0)
    {
        path.End();
        finishedPaths.Add(path);
        paths.RemoveAt(0);
    }
    else
    {
        foreach (var move in possibleMoves)
        {
            paths.Add(new Path(path, move));
        }
        paths.RemoveAt(0);
    }
} while (paths.Count > 0);

// Finished
var validPaths = finishedPaths.Where(p => p.ReachedEnd).ToList();
var bestPath = validPaths.MinBy(p => p.Length);
Console.WriteLine($"{finishedPaths.Count} paths found");
Console.WriteLine($"{validPaths.Count} valid paths found");
//foreach (var path in finishedPaths)
//{
//    if (path == bestPath)
//    {
//        Console.ForegroundColor = ConsoleColor.Green;
//    }
//    else if (path.ReachedEnd)
//    {
//        Console.ForegroundColor = ConsoleColor.Red;
//    }
//    else
//    {
//        Console.ForegroundColor = ConsoleColor.White;
//    }
//    Console.WriteLine(JsonConvert.SerializeObject(TilesToMoves(path.Tiles)));
//}
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"The Best Path is: {JsonConvert.SerializeObject(TilesToMoves(bestPath.Tiles))}");

List<MazeTile> FilterRelevantTiles(List<MazeTile> tiles)
{
    return tiles.Where(t => t.Kind == TileKind.Start || t.Kind == TileKind.Finish || t.Kind == TileKind.Dirt).ToList();
}

List<MazeTile> GetPossibleMoves(Path path)
{
    var currentTile = path.CurrentTile();
    if (currentTile.Kind == TileKind.Finish)
    {
        return new List<MazeTile>();
    }
    // Remove tiles already passed
    return currentTile.Neighbors.Where(t => path.UnpassedTile(t)).ToList();
}

List<int> TilesToMoves(HashSet<MazeTile> set)
{
    var tiles = set.ToList();
    var moves = new List<int>();
    var count = tiles.Count;
    for (int i = 0; i < count - 1; i++)
    {
        var first = tiles[i];
        var second = tiles[i + 1];
        // Top-Right
        if (second.X == (first.Y % 2 == 1 ? first.X + 1 : first.X) && second.Y == first.Y - 1) moves.Add(0);
        // Right
        if (second.X == (first.X + 1) && second.Y == first.Y) moves.Add(1);
        // Bottom-Right
        if (second.X == (first.Y % 2 == 1 ? first.X + 1 : first.X) && second.Y == first.Y + 1) moves.Add(2);
        // Bottom-Left
        if (second.X == (first.Y % 2 == 0 ? first.X - 1 : first.X) && second.Y == first.Y + 1) moves.Add(3);
        // Left
        if (second.X == (first.X - 1) && second.Y == first.Y) moves.Add(4);
        // Top-Left
        if (second.X == (first.Y % 2 == 0 ? first.X - 1 : first.X) && second.Y == first.Y - 1) moves.Add(5);
    }
    return moves;
}

void SetNeighborTiles(List<MazeTile> relevantTiles)
{
    var count = relevantTiles.Count;
    for (int i = 0; i < count; i++)
    {
        relevantTiles[i].Neighbors = GetNeighbors(relevantTiles[i]);
    }
}

MazeTile[] GetNeighbors(MazeTile tile)
{
    var tiles = new List<MazeTile>();
    // Top-Right
    tiles.Add(relevantTiles.SingleOrDefault(t => t.X == (tile.Y % 2 == 1 ? tile.X + 1 : tile.X) && t.Y == tile.Y - 1));
    // Right
    tiles.Add(relevantTiles.SingleOrDefault(t => t.X == (tile.X + 1) && t.Y == tile.Y));
    // Bottom-Right
    tiles.Add(relevantTiles.SingleOrDefault(t => t.X == (tile.Y % 2 == 1 ? tile.X + 1 : tile.X) && t.Y == tile.Y + 1));
    // Bottom-Left
    tiles.Add(relevantTiles.SingleOrDefault(t => t.X == (tile.Y % 2 == 0 ? tile.X - 1 : tile.X) && t.Y == tile.Y + 1));
    // Left
    tiles.Add(relevantTiles.SingleOrDefault(t => t.X == (tile.X - 1) && t.Y == tile.Y));
    // Top-Left
    tiles.Add(relevantTiles.SingleOrDefault(t => t.X == (tile.Y % 2 == 0 ? tile.X - 1 : tile.X) && t.Y == tile.Y - 1));
    return tiles.Where(t => t != null).ToArray();
}