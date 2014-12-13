using System;
using System.Collections.Generic;
using System.Linq;

namespace Run
{
    public class TerrainGrid : Dictionary<uint, Dictionary<uint, Tuple<char, char>>> { }

    public class TerrainSequence
    {
	    private TerrainGrid terrainData = new TerrainGrid();
	    public List<TerrainGrid> blockList { get; private set; }

        public TerrainSequence(string[] lines)
        {
            blockList = new List<TerrainGrid>();
            ParseFile(lines);
            FillData();
        }

	    private void ParseFile(string[] lines)
        {
            var buffer = new TerrainGrid();
            uint y = 0;

            foreach(var line in lines)
            {
                var yData = new Dictionary<uint, Tuple<char, char>>();
                uint x = 0;
                foreach (var element in line.Split(' '))
                {
                    if (element != "--")
                        yData.Add(x, new Tuple<char, char>((char)element[0], (char)element[1]));
                    x++;
                }
                buffer.Add(Config.SEQUENCE_HEIGHT - 1 - y, yData);
                y++;
            }

            var orderedTD = from row in buffer orderby row.Key ascending select row;
            foreach(var item in orderedTD)
                terrainData.Add(item.Key, item.Value);
        }

	    private void FillData()
        {
            var regularBlocks = new TerrainGrid();
            foreach (var row in terrainData)
            {
                var yData = new Dictionary<uint, Tuple<char, char>>();
                foreach (var col in row.Value.Where(x => x.Value.Item2 == 'X').ToList())
                    yData.Add(col.Key, col.Value);
                if(yData.Count != 0)
                    regularBlocks.Add(row.Key, yData);
            }

            if (regularBlocks.Count != 0)
            {
                SubstractBlock(terrainData, regularBlocks);
                MakeBlocks(regularBlocks);
            }

            // All remaining blocks at that point are moving blocks
            if(terrainData.Count != 0)
                MakeMovingBlocks(terrainData);
        }

        static public void SubstractBlock(TerrainGrid grid, TerrainGrid block)
        {
            // Creates a list of keys to delete in the grid from the block.
            var keysToDelete = new List<Tuple<uint, uint>>(); // <x,y>
            foreach (var row in block)
                foreach (var col in row.Value)
                    keysToDelete.Add(Tuple.Create(row.Key, col.Key));

            // Matches every element from the grid with that list
            foreach (var row in grid)
                foreach (var col in row.Value.Where(col => keysToDelete.Contains(Tuple.Create(row.Key, col.Key))).ToList())
                    row.Value.Remove(col.Key);

            // Delete all empty rows in the grid.
            foreach (var row in grid.Where(row => row.Value.Count == 0).ToList())
                grid.Remove(row.Key);
        }

	    private void MakeBlocks(TerrainGrid blocksData)
        {
	        while (blocksData.Count > 0)
	        {
                var blockItems = new List<Tuple<uint, uint, char>>(); // <x, y, projectile>
                var block = new TerrainGrid();

		        int minlength = (int)Config.SEQUENCE_SIZE;

                // Constructing our block
                foreach (var row in blocksData)
                {
                    int newItems = 0;
                    foreach (var col in row.Value)
                        if (
                        blockItems.Count == 0 || // We're always taking the first item
                        // And any other if it's directly next to it and of the same projectile kind
                        blockItems.Contains(Tuple.Create(row.Key - 1, col.Key, col.Value.Item1)) ||
                        blockItems.Contains(Tuple.Create(row.Key, col.Key - 1, col.Value.Item1)) ) 
                        {
                            blockItems.Add(Tuple.Create(row.Key, col.Key, col.Value.Item1));
                            newItems++;
                        }

                    if (newItems != 0 && newItems < minlength)
                        minlength = newItems;
                }

                // Constructing the block as a TerrainGrid
                var yData = new Dictionary<uint, Tuple<char, char>>();
                foreach (var row in blocksData)
                {
                    yData = blocksData[row.Key].Where(col => (
                        (blockItems.FindIndex(e => (e.Item1 == row.Key && e.Item2 == col.Key)) != -1))
                        && col.Key - row.Value.Keys.Min() < minlength) // Not counting overflowing cols
                            .ToDictionary(x => x.Key, x => x.Value);
                    if (yData.Count != 0)
                        block.Add(row.Key, yData);
                }

                // Substracting the block and adding it to the blockList
		        SubstractBlock(blocksData, block);
		        blockList.Add(block);
	        }
        }

	    private void MakeMovingBlocks(TerrainGrid blocksData)
        {
            int blockNumber = 0;

            while (blocksData.Count > 0)
            {
                var blockItems = new List<Tuple<uint, uint, int>>(); // <x, y, number>
                var block = new TerrainGrid();

                // Constructing our block
                foreach (var row in blocksData)
                    foreach (var col in row.Value)
                        if (
                        blockItems.Count == 0 || // We're always taking the first item
                            // And any other if it's directly next to it and of the same projectile kind
                        blockItems.Contains(Tuple.Create(row.Key - 1, col.Key, blockNumber)) ||
                        blockItems.Contains(Tuple.Create(row.Key, col.Key - 1, blockNumber))
                        )
                            blockItems.Add(Tuple.Create(row.Key, col.Key, blockNumber));

                // Constructing the block as a TerrainGrid
                var yData = new Dictionary<uint, Tuple<char, char>>();
                foreach (var row in blocksData)
                {
                    yData = blocksData[row.Key].Where(col => blockItems.FindIndex(e => (e.Item1 == row.Key && e.Item2 == col.Key)) != -1).ToDictionary(x => x.Key, x => x.Value);
                    if (yData.Count != 0)
                        block.Add(row.Key, yData);
                }

                // Substracting the block and adding it to the blockList
                SubstractBlock(blocksData, block);
                blockList.Add(block);

                blockNumber++;
            }
        }

        /*static public void PrintData(TerrainGrid data)
        {
            foreach (var row in data.Reverse())
	        {
		        Console.Write(row.Key + ": { ");
                foreach (var col in row.Value)
                    Console.Write("{ " + col.Key + ": " + col.Value.Item1 + "," + col.Value.Item2 + " } ");
		        Console.WriteLine("}");
	        }
        }*/
    }
}
