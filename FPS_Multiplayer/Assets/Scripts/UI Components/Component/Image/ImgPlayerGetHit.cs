using System.Collections;
using Events;
using Interfaces;
using Services.DependencyInjection;
using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Image
{
    public class ImgPlayerGetHit : BaseImage
    {
        [Inject] private IEntity entity;
        
        protected override void Start()
        {
            base.Start();
            Image.enabled = false;
            
            Injector.Instance.InjectSingleField(this, typeof(IEntity));
            entity.IsDamaged.AddListener(OnActivePanel);
        }

        private IEnumerator ActivePanel(bool isDamaged)
        {
            Image.enabled = isDamaged;
            yield return new WaitForSeconds(0.2f);
            entity.IsDamaged.Value = false;
        }
        
        private void OnActivePanel(bool isDamaged)
        {
            StartCoroutine(ActivePanel(isDamaged));
        }
    }
}
