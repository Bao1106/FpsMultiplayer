using Events;
using Interfaces;
using Managers;
using Services.DependencyInjection;
using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Text
{
    public class TextPlayerHealth : BaseText
    {
        [Inject] private IEntity entity;
        
        protected override async void Start()
        {
            base.Start();
            
            var playerName = PlayerManager.Instance.PlayerData.PlayerName;
            if(PlayerManager.Instance.PlayerData.IsMasterClient)
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
            entity.EntityHealth.AddListener(UpdateValue);
            UpdateValue(entity.EntityHealth.Value);
        }
        
        private void UpdateValue(int health)
        {
            ValueText.text = $"HEALTH: {health}";
        }
    }
}
