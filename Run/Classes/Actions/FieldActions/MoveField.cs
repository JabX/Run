﻿using Run.Classes.Sprites;

namespace Run.Classes.Actions.FieldActions
{
    class MoveField : FieldAction
    {
        public override void execute()
        {
            foreach (Sprite sprite in target.sprites)
		        if (sprite.type != SpriteType.PLAYER)
                    sprite.nx -= (int)target.speed; // Position will already be updated by previous SpMoves, hence nx
        }
    }
}
