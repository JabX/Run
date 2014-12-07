namespace Run.Classes.Sprites
{
    class Projectile : Sprite
    {
        public Way way { get; private set; }

        public Projectile(int x, int y, Way way) : base(1, 1, SpriteType.PROJECTILE, x, y, State.RED)
        {
            this.way = way;
        }
    }
}
