using System.Collections;
using Events;
using Interfaces;
using Managers;
using Services.DependencyInjection;
using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Image
{
    public class ImgPlayerGetHit : BaseImage
    {
        [Inject] private IEntity entity;
        
        protected override async void Start()
        {
            base.Start();
            var playerName = PlayerManager.Instance.PlayerData.PlayerName;

            ValueImage.enabled = false;
            
            if (PlayerManager.Instance.PlayerData.IsMasterClient)
            {
                await StaticEvents.SpawnPlayerCompleted.Task;
                Injector.Instance.InjectSingleField(this, typeof(IEntity));
                OnAddListener();
            }
            else
            {
                entity = (IEntity)Injector.Instance.Resolve(typeof(IEntity), playerName);
                OnAddListener();
            }
        }

        private void OnAddListener()
        {
            entity.IsDamaged.AddListener(OnActivePanel);
        }
        
        private IEnumerator ActivePanel(bool isDamaged)
        {
            ValueImage.enabled = isDamaged;
            yield return new WaitForSeconds(0.2f);
            entity.IsDamaged.Value = false;
        }
        
        private void OnActivePanel(bool isDamaged)
        {
            StartCoroutine(ActivePanel(isDamaged));
        }
    }
}
