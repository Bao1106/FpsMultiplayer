using Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Components.Base
{
    public class BaseImage : MonoBehaviour, IImageElement
    {
        public Image Image { get; set; }

        protected virtual void Start()
        {
            Image = GetComponent<Image>();
        }
    }
}
