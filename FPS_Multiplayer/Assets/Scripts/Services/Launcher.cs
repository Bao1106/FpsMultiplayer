using Infima_Games.Low_Poly_Shooter_Pack.Code.Services;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;
using AudioService = Services.IAudioService.AudioService;

namespace Services
{
    public class Launcher : MonoBehaviour
    {
        private static IAudioService _audioService;
        private static IAudioManagerService _audioManagerService;
        private static IGameModeService _gameModeService;
        
        /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Awake()
        {
            ServiceLocator.Global
                .Register(audioService = new AudioService())
                .Register(gameModeService = new GameModeService());
        }*/

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            Application.targetFrameRate = 60;
            
            #region Sound Manager Service

            //Create an object for the sound manager, and add the component!
            var soundManagerObject = new GameObject("Sound Manager");
            var soundManagerService = soundManagerObject.AddComponent<AudioManagerService>();
            
            //Make sure that we never destroy our SoundManager. We need it in other scenes too!
            DontDestroyOnLoad(soundManagerObject);
            
            #endregion
            
            ServiceLocator.Global
                .Register(_audioService = new AudioService())
                .Register(_gameModeService = new GameModeService())
                .Register(_audioManagerService = soundManagerService);
        }
    }
}
