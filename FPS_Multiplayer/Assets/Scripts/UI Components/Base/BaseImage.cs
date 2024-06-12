using Interfaces;
using Interfaces.UI;
using Unity.VisualScripting;
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
