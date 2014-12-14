﻿namespace Run.Sprites
{
    public class Obstacle : Sprite
    {
        public uint hp { get; set; }

	    public Obstacle(uint width, uint height, int x = 0, int y = 0) : base(width, height, SpriteType.Obstacle, x, y)
        {
            hp = 3;
        }
    }
}
