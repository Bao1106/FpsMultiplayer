using System;
using TMPro;
using UnityEngine;
using ITextElement = Interfaces.UI.ITextElement;
using UI_ITextElement = Interfaces.UI.ITextElement;

namespace UI_Components.Base
{
    public class BaseText : MonoBehaviour, UI_ITextElement
    {
        public TMP_Text ValueText { get; set; }

        protected virtual void Start()
        {
            ValueText = GetComponent<TMP_Text>();
        }
    }
}