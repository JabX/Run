namespace Run
{
    public abstract class Action
    {
	    public int duration { get; set; }
	    public int time { get; private set; }

	    public Action(int duration = -1)
        {
            this.duration = duration;
            time = 0;
        }
	
	    public void incTime()
        {
	        time++;
        }

        public bool isOver()
        {
	        return (time >= duration && duration != -1);
        }
    }
}
