using Events;
using Interfaces;
using Managers;
using Photon.Pun;
using Services.DependencyInjection;
using TMPro;
using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Text
{
    public class TextPlayerHealth : BaseText
    {
        private IEntity entity;

        public void Initialize(string entityName)
        {
            entity = (IEntity)Injector.Instance.Resolve(typeof(IEntity), entityName);
            if (ValueText == null) ValueText = GetComponent<TMP_Text>();
        }

        public void OnAddListener()
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
