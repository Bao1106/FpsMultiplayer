using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Button
{
    public class ButtonQuitGame : BaseButton
    {
        protected override void Start()
        {
            base.Start();
            ValueButton.onClick.AddListener(Application.Quit);
        }
    }
}