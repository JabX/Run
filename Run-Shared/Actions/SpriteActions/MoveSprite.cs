namespace Run.Actions.SpriteActions
{
    public class MoveSprite : SpriteAction
    {
        private uint speed;
	    private Direction direction;
	    private int loopTime;

	    public MoveSprite(uint speed, Direction direction, int loopTime = 0)
        {
            this.speed = speed;
            this.direction = direction;
            this.loopTime = loopTime;
        }

        public override FieldAction execute()
        {
            if (loopTime != 0 && time != 0 && time % loopTime == 0)
	        {
		        switch (direction)
		        {
		        case Direction.Down:
			        direction = Direction.Up;
			        break;
		        case Direction.Up:
			        direction = Direction.Down;
			        break;
		        case Direction.Left:
			        direction = Direction.Right;
			        break;
		        case Direction.Right:
			        direction = Direction.Left;
			        break;
		        }
	        }

	        int newX = source.x;
            int newY = source.y;

	        switch (direction)
	        {
	        case Direction.Down:
		        newY -= (int)speed;
		        break;
	        case Direction.Up:
		        newY += (int)speed;
		        break;
	        case Direction.Left:
		        newX -= (int)speed;
		        break;
	        case Direction.Right:
		        newX += (int)speed;
		        break;
	        }

            source.nx = newX;
            source.ny = newY;

	        incTime();

	        return null;
        }
    }
}
