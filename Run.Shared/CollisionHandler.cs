using System;
using System.Collections.Generic;

using Run.Colliders;

namespace Run
{
    public class CollisionHandler
    {
        private Field target { get; set; }
        public List<Collider> colliders { get; private set; }

        public CollisionHandler(Field target)
        {
            this.target = target;
            colliders = new List<Collider>();
            colliders.Add(new PlayerObstacle());
            colliders.Add(new PlayerProjectile());
            colliders.Add(new ProjectileObstacle());
        }

        private Collider getCollider(Sprite sp1, Sprite sp2)
        {
	        foreach (var collider in colliders)
	        {
		        if ((collider.signature.Item1 == sp1.type && collider.signature.Item2 == sp2.type)
                    || (collider.signature.Item2 == sp1.type && collider.signature.Item1 == sp2.type))
			        return collider;
	        }
	        return null;
        }

	    private bool areColliding(Sprite sp1, Sprite sp2)
        {
            // Check if sprite 2 collides in sprite 1 bounding box
            bool horizontal1 = ((sp1.x + sp1.width) <= sp2.x) || ((sp2.x + sp2.width) <= sp1.x);
            bool vertical1 = ((sp1.y + sp1.height) <= sp2.y) || ((sp2.y + sp2.height) <= sp1.y);

            // Check if sprite 1 collides in sprite 1 bounding box
            bool horizontal2 = ((sp2.x + sp2.width) <= sp1.x) || ((sp1.x + sp1.width) <= sp2.x);
            bool vertical2 = ((sp2.y + sp2.height) <= sp1.y) || ((sp1.y + sp1.height) <= sp2.y);

            return !(horizontal1 || vertical1 || horizontal2 || vertical2);
        }

        public void collide(Sprite sp1, Sprite sp2)
        {
            if (areColliding(sp1, sp2))
            {
                Collider collider = getCollider(sp1, sp2);
                if (collider != null)
                    collider.collide(sp1, sp2, target);
            }
        }
    }
}
