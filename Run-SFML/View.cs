using SFML.Graphics;
using SFML.Window;

using Run;
using Run.Sprites;

namespace Run_SFML
{
    class View
    {
        private RenderWindow context;
        public Field field { get; private set; }
	    public int frameSkip { get; set; }

        private Font font = new Font("../../../font.ttf");

        public View(RenderWindow context)
        {
            this.context = context;
            field = new Field();
            frameSkip = 0;
        }

	    public void draw()
        {
            context.Clear(Color.Black);

            var score = new Text(field.score.ToString(), font);
            FloatRect textRect = score.GetLocalBounds();
            score.Origin = new Vector2f(textRect.Left + textRect.Width, textRect.Top);
            score.Position = new Vector2f(Config.WindowWidth-10,10);
            context.Draw(score);

	        foreach (var sprite in field.sprites)
	        {
		        float width = (float)sprite.width * Config.Blocksize;
		        float height = (float)sprite.height * Config.Blocksize;

		        // Our block origin is bottom-left corner and SMFL's is top-left.
		        float x = (sprite.x + (sprite.nx - sprite.x) * (float)frameSkip / Config.Frameskip) * Config.Blocksize;
		        float y = context.Size.Y - (sprite.y + (sprite.ny - sprite.y) * (float)frameSkip / Config.Frameskip) * Config.Blocksize - height;

		        var shape = new RectangleShape(new Vector2f(width,height));
		        shape.Position = new Vector2f(x,y);
		        switch (sprite.state)
		        {
		            case State.Blue:
			            shape.FillColor = Color.Blue;
			            break;
                    case State.Green:
			            shape.FillColor = Color.Green;
			            break;
		            case State.Red:
			            shape.FillColor = Color.Red;
                        break;
                    case State.Yellow:
                        shape.FillColor = Color.Yellow;
                        break;
                }
                
                if(sprite.type == SpriteType.Projectile)
                {
                    var projectile = (Projectile)sprite;

                    if(projectile.way == Way.Horizontal)
                    {
                        shape.Size = new Vector2f(shape.Size.X, shape.Size.Y / 4);
			            shape.Position = new Vector2f(shape.Position.X, shape.Position.Y + 3 * Config.Blocksize / 8);
                    }
                    else
                    {
                        shape.Size = new Vector2f(shape.Size.X / 4, shape.Size.Y);
			            shape.Position = new Vector2f(shape.Position.X + 3 * Config.Blocksize / 8, shape.Position.Y);
                    }
                }
			    
		        context.Draw(shape);
	        }
        }
    }
}
