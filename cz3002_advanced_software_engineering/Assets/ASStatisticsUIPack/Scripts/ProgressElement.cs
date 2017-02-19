using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ASStatisticsUIPack
{
    [Serializable, ExecuteInEditMode]
    public abstract class ProgressElement : MonoBehaviour
    {
        [Serializable]
        public class ProgressCallback : UnityEvent<float> { }

        public ProgressCallback OnValueChange;

        [SerializeField]
        private RectTransform backgroundRect;
        public RectTransform BackgroundRect
        {
            get
            {
                return this.backgroundRect;
            }
            set
            {
                this.backgroundRect = value;
                if (this.backgroundRect != null)
                {
                    if (this.backgroundRect.gameObject.GetComponent<Image>() != null)
                    {
                        this.backgroundRect.gameObject.GetComponent<Image>().color = secondaryColor;
                    }

                    CanvasGroup cg = this.backgroundRect.gameObject.GetComponent<CanvasGroup>();
                    if (cg == null)
                    {
                        cg = this.backgroundRect.gameObject.AddComponent<CanvasGroup>();
                    }
                    cg.alpha = this.showBackgroundBar ? 1f : 0f;
                }
            }
        }
        [SerializeField]
        private RectTransform frontRect;
        public RectTransform FrontRect
        {
            get
            {
                return this.frontRect;
            }
            set
            {
                this.frontRect = value;
                if (this.frontRect != null && this.frontRect.gameObject.GetComponent<Image>() != null)
                {
                    this.frontRect.gameObject.GetComponent<Image>().color = primaryColor;
                }
            }
        }
        [SerializeField]
        private RectTransform indicatorRect;
        public RectTransform IndicatorRect
        {
            get
            {
                return this.indicatorRect;
            }
            set
            {
                this.indicatorRect = value;
                if (this.indicatorRect != null)
                {
                    this.indicatorRect.gameObject.SetActive(showIndicator);
                }
            }
        }
        [SerializeField]
        private Color primaryColor;
        public Color PrimaryColor
        {
            get
            {
                return this.primaryColor;
            }
            set
            {
                this.primaryColor = value;
                if (this.frontRect != null && this.frontRect.gameObject.GetComponent<Image>() != null)
                {
                    this.frontRect.gameObject.GetComponent<Image>().color = primaryColor;
                }
            }
        }
        [SerializeField]
        private Color secondaryColor;
        public Color SecondaryColor
        {
            get
            {
                return this.secondaryColor;
            }
            set
            {
                this.secondaryColor = value;
                if (this.backgroundRect != null && this.backgroundRect.gameObject.GetComponent<Image>() != null)
                {
                    this.backgroundRect.gameObject.GetComponent<Image>().color = secondaryColor;
                }
            }
        }
        [SerializeField]
        protected float value;
        public float Value
        {
            get
            {
                return this.value;
            }
            set
            {
                Animate(value, Application.isPlaying);
            }
        }
        [SerializeField]
        protected bool showIndicator = true;
        public bool ShowIndicator
        {
            get
            {
                return this.showIndicator;
            }
            set
            {
                this.showIndicator = value;
                if (this.indicatorRect != null)
                {
                    this.indicatorRect.gameObject.SetActive(this.showIndicator);
                }
            }
        }
        [SerializeField]
        protected bool showBackgroundBar = true;
        public bool ShowBackgroundBar
        {
            get
            {
                return this.showBackgroundBar;
            }
            set
            {
                this.showBackgroundBar = value;
                if (this.backgroundRect != null)
                {
                    CanvasGroup cg = this.backgroundRect.gameObject.GetComponent<CanvasGroup>();
                    if (cg == null)
                    {
                        cg = this.backgroundRect.gameObject.AddComponent<CanvasGroup>();
                    }
                    cg.alpha = this.showBackgroundBar ? 1f : 0f;
                }
            }
        }

        public bool AnimateAtStart = true;

        [SerializeField]
        protected float currentValue;

        private bool animate;

        public void Animate(float value, bool animate = true)
        {
            this.animate = animate;
            this.value = Mathf.Min(1, Mathf.Max(0, value));
            if (animate == false)
            {
                this.currentValue = this.value;
                UpdateUI();
            }
            NotifyListeners();
        }

        protected void NotifyListeners()
        {
            if (OnValueChange != null)
            {
                OnValueChange.Invoke(this.value);
            }
        }

        public void Update()
        {
#if UNITY_EDITOR
            FrontRect = frontRect;
            BackgroundRect = backgroundRect;
            IndicatorRect = indicatorRect;
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
            Value = value;
            ShowIndicator = showIndicator;
            ShowBackgroundBar = showBackgroundBar;
#endif
            if (Application.isPlaying)
            {
                if (animate)
                {
                    currentValue = Mathf.Lerp(currentValue, value, Time.deltaTime * 2.0f);
                    if (Mathf.Abs(currentValue - value) < 0.001f)
                    {
                        currentValue = value;
                        animate = false;
                    }
                    UpdateUI();
                }
            }
        }

        public void Start()
        {
            if (!Application.isPlaying)
                return;
            if (AnimateAtStart)
            {
                currentValue = 0f;
                animate = true;
            }
            else
            {
                currentValue = value;
                animate = false;
                UpdateUI();
            }
            NotifyListeners();
        }

        protected abstract void UpdateUI();

        protected void OnRectTransformDimensionsChange()
        {
            UpdateUI();
        }
    }
}
