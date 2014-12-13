using SFML.Graphics;
using SFML.Window;

using Run;
using Run.Sprites;
using Run.Actions.FieldActions;

namespace Run_SFML
{
    class View
    {
        private RenderWindow context;
        public Field field { get; private set; }
	    public int frameSkip { get; set; }

        public View(RenderWindow context)
        {
            this.context = context;
            field = new Field();
            frameSkip = 0;

            field.addSprite(new Player());
            field.addAction(new MoveField());
            field.addAction(new Generate());
        }

	    public void draw()
        {
            context.Clear(Color.Black);

	        foreach (var sprite in field.sprites)
	        {
		        float width = (float)sprite.width * Config.BLOCKSIZE;
		        float height = (float)sprite.height * Config.BLOCKSIZE;

		        // Our block origin is bottom-left corner and SMFL's is top-left.
		        float x = (sprite.x + (sprite.nx - sprite.x) * (float)frameSkip / Config.FRAMESKIP) * Config.BLOCKSIZE;
		        float y = context.Size.Y - (sprite.y + (sprite.ny - sprite.y) * (float)frameSkip / Config.FRAMESKIP) * Config.BLOCKSIZE - height;

		        var shape = new RectangleShape(new Vector2f(width,height));
		        shape.Position = new Vector2f(x,y);
		        switch (sprite.state)
		        {
		        case State.BLUE:
			        shape.FillColor = Color.Blue;
			        break;
		        case State.GREEN:
			        shape.FillColor = Color.Green;
			        break;
		        case State.RED:
			        shape.FillColor = Color.Red;
                    break;
                case State.YELLOW:
                    shape.FillColor = Color.Yellow;
                    break;
                }
                
                if(sprite.type == SpriteType.PROJECTILE)
                {
                    var projectile = (Projectile)sprite;

                    if(projectile.way == Way.HORIZONTAL)
                    {
                        shape.Size = new Vector2f(shape.Size.X, shape.Size.Y / 4);
			            shape.Position = new Vector2f(shape.Position.X, shape.Position.Y + 3 * Config.BLOCKSIZE / 8);
                    }
                    else
                    {
                        shape.Size = new Vector2f(shape.Size.X / 4, shape.Size.Y);
			            shape.Position = new Vector2f(shape.Position.X + 3 * Config.BLOCKSIZE / 8, shape.Position.Y);
                    }
                }
			    
		        context.Draw(shape);
	        }
        }
    }
}
