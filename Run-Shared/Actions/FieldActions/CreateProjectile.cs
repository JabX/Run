using Run.Actions.SpriteActions;
using Run.Sprites;

namespace Run.Actions.FieldActions
{
    public class CreateProjectile : FieldAction
    {
        private int x;
        private int y;
        private Direction direction;

        public CreateProjectile(int x, int y, Direction direction = Direction.RIGHT) : base(1)
        {
            this.x = x;
            this.y = y; 
            this.direction = direction; 
        }

        public override void execute()
        {
            Way way;
            if (direction == Direction.UP || direction == Direction.DOWN)
                way = Way.VERTICAL;
            else
                way = Way.HORIZONTAL;

	        Sprite newP = new Projectile(x, y, way);
	        SpriteAction move = new MoveSprite(Config.PROJECTILE_SPEED, direction);
	        newP.addAction(move);

	        // Setting up manually the next projectile position. 
	        // The next ones will be handled by the move action, but not this one since it won't be executed before the next frame.
	        int nx = x;
	        int ny = y;

	        switch (direction)
	        {
	        case Direction.DOWN:
		        ny -= (int)Config.PROJECTILE_SPEED;
		        break;
	        case Direction.UP:
		        ny += (int)Config.PROJECTILE_SPEED;
		        break;
	        case Direction.LEFT:
		        nx -= (int)Config.PROJECTILE_SPEED;
		        break;
	        case Direction.RIGHT:
		        nx += (int)Config.PROJECTILE_SPEED;
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
