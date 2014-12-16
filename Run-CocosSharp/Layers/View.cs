using System;
using System.Linq;
using CocosSharp;

using Run;
using Run.Sprites;
using Run.Actions;
using Run.Actions.SpriteActions;

namespace Run_CocosSharp
{
    public class View : CCLayerColor
    {
        public View(CCSize size) : base(size)
		{

		}

        Field field { get; set; }
        int frameSkip { get; set; }

        CCDrawNode canvas = new CCDrawNode();
        CCLabel score;
        protected override void AddedToScene()
        { 
 			base.AddedToScene();

            AddChild(canvas);

            score = new CCLabel("0", "Consolas", 22);

            score.Position = new CCPoint(Config.WindowWidth / 2, Config.WindowHeight - 25);
            score.Color = new CCColor3B(255, 255, 255);
            score.HorizontalAlignment = CCTextAlignment.Center;
            AddChild(score);

            field = new Field();
            frameSkip = 0;

            CCEventListenerKeyboard eventListener = new CCEventListenerKeyboard();
 			eventListener.OnKeyPressed = OnKeyPressed; 
 			AddEventListener(eventListener, this);

            Schedule();
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            if (field.isGameRunning)
            {
                if (frameSkip < Config.ViewFramerate / Config.Framerate - 1) frameSkip++;
                else
                {
                    field.applySpritesPosition();
                    field.executeCollisions();

                    field.executeSpriteActions();
                    field.executeFieldActions();

                    frameSkip = 0;
                }
            }
            draw();
	    }

        void draw()
        {
            canvas.Clear();

            score.Text = field.score.ToString();

	        foreach (var sprite in field.sprites)
	        {
		        float width = (float)sprite.width * Config.Blocksize;
		        float height = (float)sprite.height * Config.Blocksize;

		        float x = (sprite.x + (sprite.nx - sprite.x) * (float)frameSkip / Config.Frameskip) * Config.Blocksize;
		        float y = (sprite.y + (sprite.ny - sprite.y) * (float)frameSkip / Config.Frameskip) * Config.Blocksize;

                var color = new CCColor4B();
		        switch (sprite.state)
		        {
		            case State.Blue:
                        color.B = 255;
			            break;
                    case State.Green:
                        color.G = 255;
			            break;
		            case State.Red:
                        color.R = 255;
                        break;
                    case State.Yellow:
                        color.R = 255;
                        color.G = 255;
                        break;
                }

                if(sprite.type == SpriteType.Obstacle)
                {
                    if (color.R == 0)
                        color.R = (byte)(255 * (3 - sprite.hp) / 3);
                    color.G = (byte)(255 * (3 - sprite.hp) / 3);
                    if (color.B == 0)
                        color.B = (byte)(255 * (3 - sprite.hp) / 3);
                }

                if(sprite.type == SpriteType.Projectile)
                {
                    var projectile = (Projectile)sprite;

                    if(projectile.way == Way.Horizontal)
                    {
                        height /= 4;
                        y += 3 * Config.Blocksize / 8;
                    }
                    else
                    {
                        width /= 4;
                        x += 3 * Config.Blocksize / 8;
                    }
                }
                canvas.DrawRect(new CCRect(x, y, width, height), color);
	        }
        }

        void OnKeyPressed(CCEventKeyboard e)
        {
            if (field.isGameRunning)
            {
                var player = field.sprites.Where(sp => sp.type == SpriteType.Player).FirstOrDefault();

                // Jump
                if (e.Keys == CCKeys.Space)
                {
                    SpriteAction jump = new Jump();
                    if (player.actions.FindIndex(action => action.GetType() == typeof(Jump)) == -1)
                        player.addAction(jump);
                }

                // Fire
                if (e.Keys == CCKeys.E)
                {
                    SpriteAction fire = new FireProjectile();
                    if (player.actions.FindIndex(action => action.GetType() == typeof(FireProjectile)) == -1)
                        player.addAction(fire);
                }
            }
            else
            {
                field = new Field();
            }
        }
    }
}
