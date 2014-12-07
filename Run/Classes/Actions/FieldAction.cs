namespace Run.Classes.Actions
{
    abstract class FieldAction : Action
    {
	    public Field target { get; set; }

	    public FieldAction(int duration = -1)
        {
            this.duration = duration;
        }

	    public abstract void execute();
    }
}
