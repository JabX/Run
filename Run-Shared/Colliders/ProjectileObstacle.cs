using System;
using Run;
using Run.Sprites;

namespace Run.Colliders
{
    public class ProjectileObstacle : Collider
    {
        public ProjectileObstacle()
            : base(Tuple.Create(SpriteType.Projectile, SpriteType.Obstacle))
        {

        }
    }
}
