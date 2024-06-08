using Interfaces;
using Services.DependencyInjection;
using UI_Components.Base;

namespace UI_Components.Component.Text
{
    public class TextPlayerHealth : BaseText
    {
        //[Inject] private IEntity entity;
        
        protected override void Start()
        {
            base.Start();
            //Injector.Instance.InjectSingleField(this, typeof(IEntity));
            
            //entity.EntityHealth.AddListener(UpdateValue);
            //UpdateValue(entity.EntityHealth.Value);
        }

        private void UpdateValue(int health)
        {
            TxtValue.text = $"HEALTH: {health}";
        }
    }
}
