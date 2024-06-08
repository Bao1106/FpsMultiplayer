using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISceneInit
    {
        Task SceneInitTask { get; }
        void InitComplete();
    }
}