namespace Blockers
{
    public class UIPopupBlocker
    {
        private static bool BLOCKED;

        public static void StartBlocking() => BLOCKED = true;
        public static bool IsBlocked() => BLOCKED;
        public static void StopBlocking() => BLOCKED = false;
    }
}