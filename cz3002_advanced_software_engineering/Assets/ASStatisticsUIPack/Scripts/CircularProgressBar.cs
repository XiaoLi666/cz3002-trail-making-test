using UnityEngine;
using UnityEngine.UI;

namespace ASStatisticsUIPack
{
    public class CircularProgressBar : ProgressElement
    {
        protected override void UpdateUI()
        {
            if (FrontRect != null && FrontRect.gameObject.GetComponent<Image>() != null)
            {
                Image img = FrontRect.gameObject.GetComponent<Image>();
                img.type = Image.Type.Filled;
                img.fillMethod = Image.FillMethod.Radial360;
                img.fillAmount = currentValue;
                img.fillOrigin = 2;
                img.fillClockwise = true;
            }
        }
    }
}


