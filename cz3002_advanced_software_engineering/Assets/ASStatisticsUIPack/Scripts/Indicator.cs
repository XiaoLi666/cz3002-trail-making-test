using UnityEngine;
using UnityEngine.UI;

namespace ASStatisticsUIPack
{
    public class Indicator : MonoBehaviour
    {
        public float MaxValue = 100f;
        public string Prefix = "";
        public string Suffix = "%";
        public int DecimalPlaces = 0;
        public Text Text;

        public void UpdateIndicatorTextWithValue(float value)
        {
            if (Text != null)
            {
                Text.text = Prefix + (value * MaxValue).ToString("N" + DecimalPlaces.ToString()) + Suffix;
            }
        }
    }
}
