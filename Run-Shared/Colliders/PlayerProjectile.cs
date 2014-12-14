using System;
using Run;

namespace Run.Colliders
{
    public class PlayerProjectile : Collider
    {
        public PlayerProjectile()
            : base(Tuple.Create(SpriteType.Player, SpriteType.Projectile))
        {

        }

        public override void collide(Sprite sp1, Sprite sp2, Field target)
        {
            target.deleteSprite(sp1);
            target.deleteSprite(sp2);
            target.isGameRunning = false;
        }
    }
}
