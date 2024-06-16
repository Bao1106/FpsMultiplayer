using Events;
using Interfaces;
using Services.DependencyInjection;
using UI_Components.Base;

namespace UI_Components.Component.Text
{
    public class TextPlayerHealth : BaseText
    {
        [Inject] private IEntity entity;
        
        protected override async void Start()
        {
            await StaticEvents.SpawnPlayerCompleted.Task;
            
            base.Start();
            Injector.Instance.InjectSingleField(this, typeof(IEntity));
            
            entity.EntityHealth.AddListener(UpdateValue);
            UpdateValue(entity.EntityHealth.Value);
        }

        private void UpdateValue(int health)
        {
            ValueText.text = $"HEALTH: {health}";
        }
    }
}
