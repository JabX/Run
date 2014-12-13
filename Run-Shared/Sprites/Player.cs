namespace Run.Sprites
{
    public class Player : Sprite
    {
        public uint hp { get; set; }

        public Player() : base(1, 2, SpriteType.PLAYER, 3, 0, State.GREEN)
        {
            hp = 1;
        }
    }
}
