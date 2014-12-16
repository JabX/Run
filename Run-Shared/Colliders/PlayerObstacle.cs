using System;
using Run;

namespace Run.Colliders
{
    public class PlayerObstacle : Collider
    {
        public PlayerObstacle() : base(Tuple.Create(SpriteType.Player, SpriteType.Obstacle))
        {

        }
    }
}
