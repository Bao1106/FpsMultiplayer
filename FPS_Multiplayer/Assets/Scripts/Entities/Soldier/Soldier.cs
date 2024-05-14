using Entities.Base;
using Services;
using UnityEngine;

namespace Entities.Soldier
{
    public class Soldier : Entity
    {
        private IAudioService audioService;
        
        protected override void Start()
        {
            base.Start();
            GetService();
        }

        private void GetService()
        {
            ServiceLocator.For(this).Get(out audioService);
            audioService.AudioSource = GetComponent<AudioSource>();
        }

        private void PlayWalkSound()
        {
            audioService.PlaySound(IAudioService.AudioType.Walk);
        }
    }
}
