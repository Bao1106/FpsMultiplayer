using System;
using Interfaces.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Components.Base
{
    public class BaseButton : MonoBehaviour, IButtonElement
    {
        public Button ValueButton { get; set; }

        protected virtual void Start()
        {
            ValueButton = GetComponent<Button>();
        }
    }
}