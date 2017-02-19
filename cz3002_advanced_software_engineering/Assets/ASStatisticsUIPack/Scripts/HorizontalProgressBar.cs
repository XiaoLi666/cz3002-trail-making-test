using UnityEngine;

namespace ASStatisticsUIPack
{
    public class HorizontalProgressBar : ProgressElement
    {
        protected override void UpdateUI()
        {
            if (BackgroundRect != null && FrontRect != null)
            {
                float maxWidth = BackgroundRect.rect.width;
                float height = FrontRect.sizeDelta.y;
                FrontRect.sizeDelta = new Vector2(maxWidth * currentValue, height);
            }
        }
    }
}
