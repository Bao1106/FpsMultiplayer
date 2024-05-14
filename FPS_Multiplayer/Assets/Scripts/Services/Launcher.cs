using UnityEngine;
using AudioService = Services.IAudioService.AudioService;

namespace Services
{
    public class Launcher : MonoBehaviour
    {
        private IAudioService audioService;

        private void Awake()
        {
            ServiceLocator.Global.Register(audioService = new AudioService());
        }
    }
}
