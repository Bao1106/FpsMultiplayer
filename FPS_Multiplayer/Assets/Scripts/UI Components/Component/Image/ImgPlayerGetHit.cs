using System.Collections;
using Events;
using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Image
{
    public class ImgPlayerGetHit : BaseImage
    {
        protected override void Start()
        {
            base.Start();
            Image.enabled = false;
            
            StaticEvents.IsDamaged.AddListener(OnActivePanel);
        }

        private IEnumerator ActivePanel(bool isDamaged)
        {
            Image.enabled = isDamaged;
            yield return new WaitForSeconds(0.2f);
            StaticEvents.IsDamaged.Value = false;
        }
        
        private void OnActivePanel(bool isDamaged)
        {
            StartCoroutine(ActivePanel(isDamaged));
        }
    }
}
