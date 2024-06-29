using System.Threading.Tasks;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class SceneInitManager : MonoBehaviour, ISceneInit
    {
        private readonly TaskCompletionSource<bool> taskCompletion = new();
        public Task SceneInitTask => taskCompletion.Task;
        public void InitComplete()
        {
            taskCompletion.SetResult(true);
        }
    }
}