using System;
using Run;

namespace Run.Colliders
{
    public class PlayerObstacle : Collider
    {
        public PlayerObstacle() : base(Tuple.Create(SpriteType.Player, SpriteType.Obstacle))
        {

        }

        public override void collide(Sprite sp1, Sprite sp2, Field target)
        {
	        target.deleteSprite(sp1);
            target.isGameRunning = false;
        }
    }
}
