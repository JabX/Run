using System;
using System.Linq;
using Run;

namespace Run.Actions.FieldActions
{
    public class Generate : FieldAction
    {
        private uint blockCount = 0;
	    private int complexity = 0;

	    private void createSequence()
        {
            foreach (var obstacle in target.getSequence(complexity))
	        {
		        if (obstacle.Values.First().Values.First().Item2 == 'X')
		        {
			        Sprite sp = target.MakeRegularBlock(obstacle);
			        target.addSprite(sp);
		        }
		        else
		        {
			        Sprite sp = target.MakeMovingBlock(obstacle);
			        target.addSprite(sp);
		        }
	        }
        }

        public override void execute()
        {
            if (blockCount == 0)
                createSequence();

            target.deleteOutOfBoundSprites();

            blockCount += target.speed;
            if (blockCount >= Config.SequenceWidth)
                blockCount = 0;

            incTime();

            if (time % Config.ComplexityIncTime == 0)
                complexity++;

            if (time % Config.SpeedIncTime == 0 && target.speed < Config.MaxSpeed)
                target.incSpeed();
        }
    }
}
