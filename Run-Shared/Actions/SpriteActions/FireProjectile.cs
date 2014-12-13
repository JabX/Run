using Run.Actions.FieldActions;

namespace Run.Actions.SpriteActions
{
    public class FireProjectile : SpriteAction
    {
        private uint fireRate;
        private Direction direction;

        public FireProjectile(Direction direction = Direction.RIGHT, uint fireRate = 0) : base((fireRate == 0) ? 1 : -1)
        {
            this.direction = direction;
            this.fireRate = fireRate;
        }

        public override FieldAction execute()
        {
            incTime();

            if (fireRate == 0)
                return new CreateProjectile(source.x, source.y, direction);

            else if (time % fireRate == 0)
                return new CreateProjectile(source.x, source.y, direction);
            else
                return null;
        }
    }
}
