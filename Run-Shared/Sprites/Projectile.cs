namespace Run.Sprites
{
    public class Projectile : Sprite
    {
        public Way way { get; private set; }

        public Projectile(int x, int y, Way way) : base(1, 1, SpriteType.Projectile, x, y, State.Red)
        {
            this.way = way;
        }
    }
}
