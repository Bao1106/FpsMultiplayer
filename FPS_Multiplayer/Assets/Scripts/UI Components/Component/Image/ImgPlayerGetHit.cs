using System.Collections;
using Interfaces;
using Services.DependencyInjection;
using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Image
{
    public class ImgPlayerGetHit : BaseImage
    {
        private IEntity entity;

        public void Initialize(string entityName)
        {
            entity = (IEntity)Injector.Instance.Resolve(typeof(IEntity), entityName);
            if (ValueImage == null) ValueImage = GetComponent<UnityEngine.UI.Image>();
        }

        public void OnAddListener()
        {
            ValueImage.enabled = false;
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
