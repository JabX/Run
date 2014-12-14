using Run.Actions.SpriteActions;
using Run.Sprites;

namespace Run.Actions.FieldActions
{
    public class CreateProjectile : FieldAction
    {
        private int x;
        private int y;
        private Direction direction;

        public CreateProjectile(int x, int y, Direction direction = Direction.Right) : base(1)
        {
            this.x = x;
            this.y = y; 
            this.direction = direction; 
        }

        public override void execute()
        {
            Way way;
            if (direction == Direction.Up || direction == Direction.Down)
                way = Way.Vertical;
            else
                way = Way.Horizontal;

	        Sprite newP = new Projectile(x, y, way);
	        SpriteAction move = new MoveSprite(Config.ProjectileSpeed, direction);
	        newP.addAction(move);

	        // Setting up manually the next projectile position. 
	        // The next ones will be handled by the move action, but not this one since it won't be executed before the next frame.
	        int nx = x;
	        int ny = y;

	        switch (direction)
	        {
	        case Direction.Down:
		        ny -= (int)Config.ProjectileSpeed;
		        break;
	        case Direction.Up:
                ny += (int)Config.ProjectileSpeed;
		        break;
	        case Direction.Left:
                nx -= (int)Config.ProjectileSpeed;
		        break;
	        case Direction.Right:
                nx += (int)Config.ProjectileSpeed;
		        break;
	        }
            nx -= (int)target.speed;

            newP.nx = nx;
            newP.ny = ny;
	        target.addSprite(newP);

	        incTime();
        }
    }
}
