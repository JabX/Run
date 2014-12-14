namespace Run
{
    public class Config
    {
        public static uint Blocksize = 30;
        public static uint WindowWidth = 1260;
        public static uint WindowHeight = 390;

        public static uint ViewFramerate = 60;
        public static uint Framerate = 10;
        public static uint Frameskip = ViewFramerate / Framerate;

        public static uint BaseSpeed = 2;
        public static uint MaxSpeed = 3;

        public static uint ProjectileSpeed = 8;

        public static uint SequenceWidth = 30;
        public static uint SequenceHeight = 7;

        public static uint ComplexityIncTime = 60;
        public static uint SpeedIncTime = 300;

        public static uint WindowBlockWidth = WindowWidth / Blocksize;
        public static uint WindowBlockHeight = WindowHeight / Blocksize;
    }
}
