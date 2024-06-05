using Services;

namespace Events
{
    public abstract class StaticEvents
    {
        public static Observer<int> PlayerHealth;
        public static Observer<bool> IsDamaged;
        public static bool IsUserInRange;
    }
}