namespace Blockers
{
    public class GameBlocker
    {
        private static int BLOCKER_COUNT;

        public static void StartBlocking() => BLOCKER_COUNT++;
        public static bool IsBlocked() => BLOCKER_COUNT > 0;
        public static void StopBlocking()
        {
            if (BLOCKER_COUNT > 0)
                BLOCKER_COUNT--;
        }
    }
}