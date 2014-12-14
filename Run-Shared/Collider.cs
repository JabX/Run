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

        public abstract void collide(Sprite sp1, Sprite sp2, Field target);
    }
}
