using System.Threading.Tasks;
using Services;

namespace Events
{
    public abstract class StaticEvents
    {
        public static readonly TaskCompletionSource<bool> SpawnPlayerCompleted = new ();
    }
}