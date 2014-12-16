namespace Run.Actions.SpriteActions
{
    public class Jump : SpriteAction
    {
        private int height;

	    public Jump(int height = 4) : base((int)Config.Framerate/3)
        {
            this.height = height;
        }

        public override FieldAction execute()
        {
            int thisHeight = (4 * height / duration) * (-(time * time) / duration + time);
            int nextHeight = (4 * height / duration) * (-((time + 1) * (time + 1)) / duration + time + 1);

            incTime();
            if (time <= duration)
                source.ny += nextHeight - thisHeight;

            return null;
        }
    }
}
