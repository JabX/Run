namespace Run
{
    class Config
    {
        public static uint BLOCKSIZE = 30;
        public static uint WINDOW_WIDTH = 1260;
        public static uint WINDOW_HEIGHT = 390;

        public static uint VIEW_FRAMERATE = 60;
        public static uint FRAMERATE = 10;
        public static uint FRAMESKIP = (VIEW_FRAMERATE / FRAMERATE);

        public static uint BASE_SPEED = 2;
        public static uint MAX_SPEED = 2;

        public static uint PROJECTILE_SPEED = 8;

        public static uint SEQUENCE_SIZE = 30;
        public static uint SEQUENCE_HEIGHT = 7;
        public static uint SEQ_C0_COUNT = 4;

        public static uint COMPLEXITY_INC_TIME = 60;
        public static uint SPEED_INC_TIME = 300;

        public static uint WINDOW_BLOCK_WIDTH = (WINDOW_WIDTH / BLOCKSIZE);
        public static uint WINDOW_BLOCK_HEIGHT = (WINDOW_HEIGHT / BLOCKSIZE);
    }
}
