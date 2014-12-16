using System;

namespace Run
{
    public abstract class Collider
    {
        public Tuple<SpriteType, SpriteType> signature { get; private set; }

        public Collider(Tuple<SpriteType, SpriteType> signature)
        {
            this.signature = signature;
        }

        public virtual void collide(Sprite sp1, Sprite sp2, Field target)
        {
            sp1.hp -= 1;
            sp2.hp -= 1;
        }
    }
}
