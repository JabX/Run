namespace Run.Actions.FieldActions
{
    class UpdateScore : FieldAction
    {
        public override void execute()
        {
            target.score += 7;
        }
    }
}
