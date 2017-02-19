using UnityEngine;

namespace ASStatisticsUIPack
{
    public class VerticalProgressBar : ProgressElement
    {
        protected override void UpdateUI()
        {
            if (BackgroundRect != null && FrontRect != null)
            {
                float maxHeight = BackgroundRect.rect.height;
                float width = FrontRect.sizeDelta.x;
                FrontRect.sizeDelta = new Vector2(width, maxHeight * currentValue);
            }
        }
    }
}
