namespace Run.Classes.Actions.SpriteActions
{
    class MoveSprite : SpriteAction
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
		        case Direction.DOWN:
			        direction = Direction.UP;
			        break;
		        case Direction.UP:
			        direction = Direction.DOWN;
			        break;
		        case Direction.LEFT:
			        direction = Direction.RIGHT;
			        break;
		        case Direction.RIGHT:
			        direction = Direction.LEFT;
			        break;
		        }
	        }

	        int newX = source.x;
            int newY = source.y;

	        switch (direction)
	        {
	        case Direction.DOWN:
		        newY -= (int)speed;
		        break;
	        case Direction.UP:
		        newY += (int)speed;
		        break;
	        case Direction.LEFT:
		        newX -= (int)speed;
		        break;
	        case Direction.RIGHT:
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
