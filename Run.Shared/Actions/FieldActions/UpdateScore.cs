namespace Run.Actions.FieldActions
{
    class UpdateScore : FieldAction
    {
        public override void execute()
        {
            target.score += (uint)(7 * time * target.speed / 100);
            incTime();
        }
    }
}
