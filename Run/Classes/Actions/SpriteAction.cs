namespace Run.Classes.Actions
{
    abstract class SpriteAction : Action
    {
        public Sprite source { get; set; }

	    public SpriteAction(int duration = -1)
        {
            this.duration = duration;
        }

	    public abstract FieldAction execute();
    }
}
