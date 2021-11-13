namespace HexaMaze
{
    public static class PathFinder
    {
        public static List<MazeTile> FilterRelevantTiles(List<MazeTile> tiles)
        {
            return tiles.Where(t => t.Kind == TileKind.Start || t.Kind == TileKind.Finish || t.Kind == TileKind.Dirt).ToList();
        }

        public static void SetNeighborTiles(List<MazeTile> relevantTiles)
        {
            var count = relevantTiles.Count;
            for (int i = 0; i < count; i++)
            {
                relevantTiles[i].Neighbors = GetNeighbors(relevantTiles[i]);
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
        }
        public static List<int> TilesToMoves(List<MazeTile> set)
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
    }
}
