using System;
using System.Linq;

using Run;
using Run.Actions;
using Run.Actions.SpriteActions;

using SFML.Graphics;
using SFML.Window;

namespace Run_SFML
{
    class Program
    {
        static private RenderWindow window = null;
        static private View mainView = null;

        static void OnClosed(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.Close();
        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (mainView.field.isGameRunning)
            {
                var player = mainView.field.sprites.Where(sp => sp.type == SpriteType.Player).FirstOrDefault();

                // Jump
                if (e.Code == Keyboard.Key.Space)
                {
                    SpriteAction jump = new Jump();
                    if (player.actions.FindIndex(action => action.GetType() == typeof(Jump)) == -1)
                        player.addAction(jump);
                }

                // Fire
                if (e.Code == Keyboard.Key.E)
                {
                    SpriteAction fire = new FireProjectile();
                    if (player.actions.FindIndex(action => action.GetType() == typeof(FireProjectile)) == -1)
                        player.addAction(fire);
                }
            }
            else
            {
                mainView = new View(window);
            }
		}

        static void Main(string[] args)
        {
	        window = new RenderWindow(new VideoMode(Config.WindowWidth, Config.WindowHeight), "Run");
	        // window.SetFramerateLimit(Config.VIEW_FRAMERATE);

	        mainView = new View(window);

            window.SetKeyRepeatEnabled(false);
            window.SetVerticalSyncEnabled(true);

            window.Closed += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);

	        while (window.IsOpen())
	        {
                window.DispatchEvents();

                if (mainView.field.isGameRunning)
                {
                    // View is rocking a VIEW_FRAMERATE framerate (probably 60fps)
                    // But we're effectively acting only at a FRAMERATE framerate (meaning we're handling things every 3-4 frames)
                    if (mainView.frameSkip < Config.ViewFramerate / Config.Framerate - 1) mainView.frameSkip++;
                    else
                    {
                        mainView.field.applySpritesPosition();
                        mainView.field.executeCollisions();

                        mainView.field.executeSpriteActions();
                        mainView.field.executeFieldActions();

                        mainView.frameSkip = 0;
                    }
                }

		        // Redraw and display current frame
		        mainView.draw();
		        window.Display();
	        }
        }
    }
}
