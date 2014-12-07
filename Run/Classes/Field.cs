﻿using System;
using System.Collections.Generic;
using System.Linq;
using Run.Classes.Actions;
using Run.Classes.Actions.SpriteActions;
using Run.Classes.Sprites;

namespace Run.Classes
{
    class Field
    {
	    public List<Sprite> sprites { get; private set; }
	    public List<FieldAction> actions { get; private set; }

        public uint speed { get; private set; }

        // public CollisionHandler collisionHandler { get; set; }

        public List<List<TerrainGrid>> sequences { get; set; }

	    public Field()
        {
            speed = Config.BASE_SPEED;
            sprites = new List<Sprite>();
            actions = new List<FieldAction>();
            // collisionHandler = new CollisionHandler(this);

            sequences = new List<List<TerrainGrid>>();

            for (uint h = 0; h < 4; h++)
                for (uint i = 0; i < Config.SEQ_C0_COUNT; i++)
                {
                    var seqTest = new TerrainSequence("Terrain/terrain" + h.ToString() + "-" + i.ToString() + ".txt");
                    sequences.Add(seqTest.blockList);
                }
        }
	
	    public List<TerrainGrid> getSequence(int complexity)
        {
            var rand = new Random();
            int c = (complexity > 3) ? 3 : complexity;
            return sequences[rand.Next(0, (c + 1) * (int)Config.SEQ_C0_COUNT)];
        }

        public Sprite MakeRegularBlock(TerrainGrid block)
        {
            int y = (int)block.Keys.First();
            int x = (int)block.Values.First().Keys.First();
	        uint height = (uint)block.Count;
            uint width = (uint)block.Values.First().Count();

	        Sprite output = new Obstacle(width, height, x+(int)Config.WINDOW_BLOCK_WIDTH, y);

            char fireDirection = block.Values.First().Values.First().Item1;
	        if (fireDirection != 'X')
	        {
		        output.state = State.RED;

                Direction direction = Direction.UP;
		        switch (fireDirection)
		        {
		            case 'D':
			            direction = Direction.DOWN;
			            break;
		            case 'L':
			            direction = Direction.LEFT;
			            break;
		            case 'R':
			            direction = Direction.RIGHT;
			            break;
		        }
		        SpriteAction fire = new FireProjectile(direction, Config.FRAMERATE/2);
		        output.addAction(fire);
	        }

	        return output;
        }

        public Sprite MakeMovingBlock(TerrainGrid block)
        {
            var blockItems = new List<Tuple<uint, uint, char>>(); // <x, y, projectile>
	        // Constructing our block
            foreach (var row in block)
            {
                foreach (var col in row.Value)
                    if (
                    blockItems.Count == 0 || // We're always taking the first item
                    // And any other if it's directly next to it and of the same projectile kind
                    blockItems.Contains(Tuple.Create(row.Key - 1, col.Key, col.Value.Item1)) ||
                    blockItems.Contains(Tuple.Create(row.Key, col.Key - 1, col.Value.Item1))
                    )
                        blockItems.Add(Tuple.Create(row.Key, col.Key, col.Value.Item1));
            }

            // Constructing the block as a TerrainGrid
            var yData = new Dictionary<uint, Tuple<char, char>>();
            var movingBlock = new TerrainGrid();

            foreach (var row in block)
            {
                yData = block[row.Key].Where(col => (blockItems.FindIndex(e => (e.Item1 == row.Key && e.Item2 == col.Key)) != -1)).ToDictionary(x => x.Key, x => x.Value);
                if (yData.Count != 0)
                    movingBlock.Add(row.Key, yData);
            }

	        var output = MakeRegularBlock(movingBlock);

	        Direction direction;
	        bool isDirectionUp = false;
	        int loopTime = 0;

	        foreach (var row in block)
		        if (row.Value.Values.First().Item1 == '+')
		        {
			        isDirectionUp = true;
			        loopTime++;
		        }

	        if (isDirectionUp)
		        direction = Direction.UP;
	        else
	        {
		        direction = Direction.RIGHT;
                foreach (var col in block.Values.First())
                    if (col.Value.Item1 == '+')
                        loopTime++;
	        }

	        SpriteAction loopMove = new MoveSprite(1, direction, loopTime);
	        output.addAction(loopMove);
	        return output;
        }

	    public void incSpeed()
        {
            speed++;
        }

        public void addSprite(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        public void deleteOutOfBoundSprites()
        {
            sprites.RemoveAll(sprite => 
                                sprite.x + sprite.width < -10
                            ||  sprite.x > Config.WINDOW_BLOCK_WIDTH + Config.SEQUENCE_SIZE + 10
                            ||  sprite.y > Config.WINDOW_BLOCK_HEIGHT + 5
                            ||  sprite.y < -5);
        }

        public void applySpritesPosition()
        {
            foreach (Sprite sprite in sprites)
                sprite.applyPosition();
        }

        public void addAction(FieldAction action)
        {
            action.target = this;
            actions.Add(action);
        }

        public void executeFieldActions()
        {
            foreach (var action in actions)
                action.execute();

            var actionsOver = actions.Where(action => action.isOver()).ToList();
            foreach(var action in actionsOver)
                if (action.isOver())
                    actions.Remove(action);
        }

        public void executeSpriteActions()
        {
            foreach (Sprite sprite in sprites)
	        {
		        List<FieldAction> fieldActions = sprite.executeActions();

		        if (!fieldActions.Equals(null))
			        foreach (FieldAction action in fieldActions)
				        addAction(action);
	        }
        }

        /* public void executeCollisions()
        {
            for (uint index1 = 0; index1 < sprites.Count; index1++)
		        for (uint index2 = index1 + 1; index2 < sprites.Count; index2++) 
                {
			        uint indexDifference = index2 - index1;
			        if (indexDifference > 0)
				        collisionHandler.executeCollider(sprites[index1], sprites[index2]);
		        }
        } */

        public int updateScore()
        {
            return 0;
        }
    }
}
