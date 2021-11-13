namespace HexaMaze
{
    // Based on https://www.redblobgames.com/pathfinding/a-star/implementation.html
    public static class DijkstrasAlgorithm
    {
        public static void Run(List<MazeTile> level)
        {
            var relevantTiles = PathFinder.FilterRelevantTiles(level);
            PathFinder.SetNeighborTiles(relevantTiles);
            var startTile = relevantTiles.Single(t => t.Kind == TileKind.Start);
            var goalTile = relevantTiles.Single(t => t.Kind == TileKind.Finish);
            var result = DijkstraSearch();
            var path = ReconstructPath(result.Item1);
            Console.WriteLine(JsonConvert.SerializeObject(PathFinder.TilesToMoves(path)));

            (Dictionary<MazeTile, MazeTile>, Dictionary<MazeTile, int>) DijkstraSearch()
            {
                var tiles = new List<(MazeTile, int)>();
                tiles.Add(new (startTile, 0));
                var cameForm = new Dictionary<MazeTile,MazeTile>();
                var costSoFar = new Dictionary<MazeTile, int>();
                cameForm[startTile] = null;
                costSoFar[startTile] = 0;
                while (tiles.Count != 0)
                {
                    var current = tiles.First();
                    tiles.Remove(current);

                    if (current.Item1 == goalTile)
                    {
                        break;
                    }

                    foreach (var neighbor in current.Item1.Neighbors)
                    {
                        var newCost = costSoFar[current.Item1] + 1;
                        if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                        {
                            costSoFar[neighbor] = newCost;
                            var priority = newCost;
                            tiles.Add(new (neighbor, priority));
                            cameForm[neighbor] = current.Item1;
                        }
                    }
                }
                return new(cameForm, costSoFar);
            }

            List<MazeTile> ReconstructPath(Dictionary<MazeTile, MazeTile> cameForm)
            {
                var current = goalTile;
                var path = new List<MazeTile>();
                while (current != startTile)
                {
                    path.Add(current);
                    current = cameForm[current];
                }
                path.Add(startTile);
                path.Reverse();
                return path;
            }
        }
    }
}
