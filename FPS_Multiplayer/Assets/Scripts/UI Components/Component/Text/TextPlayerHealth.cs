using Entities.Entity;
using Events;
using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Text
{
    public class TextPlayerHealth : BaseText
    {
        protected override void Start()
        {
            base.Start();
            StaticEvents.PlayerHealth.AddListener(UpdateValue);
            
            UpdateValue(StaticEvents.PlayerHealth.Value);
        }

        private void UpdateValue(int health)
        {
            TxtValue.text = $"HEALTH: {health}";
        }
    }
}
