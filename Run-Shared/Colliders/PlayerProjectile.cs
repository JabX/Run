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
    }
}
