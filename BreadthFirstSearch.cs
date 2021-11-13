namespace HexaMaze
{
    // Based on https://www.redblobgames.com/pathfinding/a-star/implementation.html
    public static class BreadthFirstSearch
    {
        public static void Run(List<MazeTile> level)
        {
            var relevantTiles = PathFinder.FilterRelevantTiles(level);
            PathFinder.SetNeighborTiles(relevantTiles);
            var startTile = relevantTiles.Single(t => t.Kind == TileKind.Start);
            var goalTile = relevantTiles.Single(t => t.Kind == TileKind.Finish);
            var path = BreadthFirstSearch();

            Dictionary<MazeTile, MazeTile> BreadthFirstSearch()
            {
                var tiles = new List<MazeTile>() { startTile };
                var cameForm = new Dictionary<MazeTile, MazeTile>();
                cameForm[startTile] = null;
                while (tiles.Count != 0)
                {
                    var current = tiles.First();
                    tiles.RemoveAt(0);
                    if (current == goalTile)
                    {
                        break;
                    }
                    Console.WriteLine("Visiting: " + current.X + " " + current.Y);
                    foreach (var neighbor in current.Neighbors)
                    {
                        if (!cameForm.ContainsKey(neighbor))
                        {
                            tiles.Add(neighbor);
                            cameForm[neighbor] = current;
                        }
                    }
                }
                return cameForm;
            }

        }
    }
}
