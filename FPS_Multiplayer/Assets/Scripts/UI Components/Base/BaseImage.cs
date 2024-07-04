using Interfaces.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Components.Base
{
    public class BaseImage : MonoBehaviour, IImageElement
    {
        public Image ValueImage { get; set; }

        protected virtual void Start()
        {
            ValueImage = GetComponent<Image>();
        }
    }
}
