using System;
using TMPro;
using UnityEngine;
using ITextElement = Interfaces.ITextElement;

namespace UI_Components.Base
{
    public class BaseText : MonoBehaviour, ITextElement
    {
        public TMP_Text TxtValue { get; set; }

        protected virtual void Start()
        {
            TxtValue = GetComponent<TMP_Text>();
        }
    }
}