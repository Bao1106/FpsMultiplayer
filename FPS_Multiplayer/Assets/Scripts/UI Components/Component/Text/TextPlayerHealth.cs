using Entities.Base;
using Entities.Entity;
using Events;
using Interfaces;
using Services;
using Services.DependencyInjection;
using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Text
{
    public class TextPlayerHealth : BaseText
    {
        [Inject] private IEntity entity;
        
        protected override void Start()
        {
            base.Start();
            Injector.Instance.InitializeInjector();
            
            entity.EntityHealth.AddListener(UpdateValue);
            UpdateValue(entity.EntityHealth.Value);
        }

        private void UpdateValue(int health)
        {
            TxtValue.text = $"HEALTH: {health}";
        }
    }
}
