//Copyright 2022, Infima Games. All Rights Reserved.

using Infima_Games.Low_Poly_Shooter_Pack.Code.Services;
using InfimaGames.LowPolyShooterPack;
using Services;
using UnityEngine;
using AudioSettings = Infima_Games.Low_Poly_Shooter_Pack.Code.Services.AudioSettings;

namespace Infima_Games.Low_Poly_Shooter_Pack.Code.Animation
{
    /// <summary>
    /// Play Sound Behaviour. Plays an AudioClip using our custom AudioManager!
    /// </summary>
    public class PlaySoundBehaviour : StateMachineBehaviour
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "Setup")]
        
        [Tooltip("AudioClip to play!")]
        [SerializeField]
        private AudioClip clip;
        
        [Title(label: "Settings")]

        [Tooltip("Audio Settings.")]
        [SerializeField]
        private AudioSettings settings = new AudioSettings(1.0f, 0.0f, true);

        /// <summary>
        /// Audio Manager Service. Handles all game audio.
        /// </summary>
        private IAudioManagerService audioManagerService;

        #endregion

        #region UNITY

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ServiceLocator.Global.Get(out audioManagerService);

            //Play!
            audioManagerService?.PlayOneShot(clip, settings);
        }

        #endregion
    }
}