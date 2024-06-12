using UI_Components.Base;
using UnityEngine;

namespace UI_Components.Component.Text
{
    public class TextTitle : BaseText
    {
        private readonly string strColor = "#8E0000";
        
        protected override void Start()
        {
            base.Start();
            ChangeColor(ValueText.text, ValueText.fontSize);
        }

        private void ChangeColor(string title, float size)
        {
            var highlightedText = title.ToUpper()
                .Replace("F", $"<color={strColor}><size={size + 30}>F</size></color>")
                .Replace("Z", $"<color={strColor}><size={size + 30}>Z</size></color>");

            ValueText.text = highlightedText;
        }
    }
}
