using System.Collections.Generic;
using System.Linq;

using Run.Actions;

namespace Run
{
    public abstract class Sprite
    {
        public int x { get; set; }
        public int y { get; set; }
        public int nx { get; set; }
        public int ny { get; set; }
        public uint width { get; set; }
        public uint height { get; set; }
        public State state { get; set; }
        public SpriteType type { get; private set; }
        public List<SpriteAction> actions { get; private set; }

        public Sprite(uint width, uint height, SpriteType type, int x = 0, int y = 0, State state = State.BLUE)
        {
            this.width = width;
            this.height = height;
            this.type = type;
            this.x = x;
            this.y = y;
            nx = x;
            ny = y;
            this.state = state;
            actions = new List<SpriteAction>();
        }

	    public void applyPosition()
        {
            x = nx;
            y = ny;
        }

	    public void addAction(SpriteAction action)
        {
            action.source = this;
	        actions.Add(action);
        }

	    public List<FieldAction> executeActions()
        {
            List<FieldAction> fieldActions = new List<FieldAction>();

            foreach (var action in actions)
            {
                var fieldAction = action.execute();

                if (fieldAction != null)
                    fieldActions.Add(fieldAction);
            }

            var actionsOver = actions.Where(action => action.isOver()).ToList();
            foreach (var action in actionsOver)
            {
		        if (action.isOver())
			        actions.Remove(action);
            }

            return fieldActions; 
        }
    }
}