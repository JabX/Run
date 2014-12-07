using System;
using System.Linq;

using Run.Classes;
using Run.Classes.Actions;
using Run.Classes.Actions.SpriteActions;

using SFML.Graphics;
using SFML.Window;

namespace Run
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
            var player = mainView.field.sprites.Where(sp => sp.type == SpriteType.PLAYER).FirstOrDefault();

            // Jump
			if (e.Code == Keyboard.Key.Space)
            {
			    SpriteAction jump = new Jump();
				if(player.actions.FindIndex(action => action.GetType() == typeof(Jump)) == -1)
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

        static void Main(string[] args)
        {
	        window = new RenderWindow(new VideoMode(Config.WINDOW_WIDTH, Config.WINDOW_HEIGHT), "Run");
	        // window.SetFramerateLimit(Config.VIEW_FRAMERATE);

	        mainView = new View(window);

            window.SetKeyRepeatEnabled(false);
            window.SetVerticalSyncEnabled(true);

            window.Closed += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);

	        while (window.IsOpen())
	        {
		        window.DispatchEvents();

		        // View is rocking a VIEW_FRAMERATE framerate (probably 60fps)
		        // But we're effectively acting only at a FRAMERATE framerate (meaning we're handling things every 3-4 frames)
		        if (mainView.frameSkip < Config.VIEW_FRAMERATE / Config.FRAMERATE - 1) mainView.frameSkip++;
		        else
		        {
			        mainView.field.applySpritesPosition();
			        //mainView.field.executeCollisions();

			        mainView.field.executeSpriteActions();
			        mainView.field.executeFieldActions();
			
			        mainView.frameSkip = 0;
		        }

		        // Redraw and display current frame
		        mainView.draw();
		        window.Display();
	        }
        }
    }
}
