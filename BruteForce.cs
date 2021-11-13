namespace HexaMaze
{
    public static class BruteForce
    {
        public static void Run(List<MazeTile> level)
        {
            var relevantTiles = PathFinder.FilterRelevantTiles(level);
            PathFinder.SetNeighborTiles(relevantTiles);
            var startTile = relevantTiles.Single(t => t.Kind == TileKind.Start);
            var goalTile = relevantTiles.Single(t => t.Kind == TileKind.Finish);

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
            foreach (var path in finishedPaths)
            {
                if (path == bestPath)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (path.ReachedEnd)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(JsonConvert.SerializeObject(PathFinder.TilesToMoves(path.Tiles.ToList())));
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"The Best Path is: {JsonConvert.SerializeObject(PathFinder.TilesToMoves(bestPath.Tiles.ToList()))}");

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
        }
    }
}
