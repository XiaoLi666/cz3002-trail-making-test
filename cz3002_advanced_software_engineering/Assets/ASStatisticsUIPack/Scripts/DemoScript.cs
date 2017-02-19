using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ASStatisticsUIPack
{
    public class DemoScript : MonoBehaviour
    {
        public List<HorizontalProgressBar> ScoreProgressBars = new List<HorizontalProgressBar>();
        public List<Text> ScoreTextElements = new List<Text>();
        private List<int> scores = new List<int>();

        public List<VerticalProgressBar> DiagramProgressBars = new List<VerticalProgressBar>();
        public Text IndicatorText;
        public Text DiagramCaption;
        private List<float> diagramValues = new List<float>();

        public Text KillsText;
        private int kills;

        public Text DeathsText;
        private int deaths;

        public Text KillDeathRatioText;
        public CircularProgressBar KillDeathRatioProgressBar;
        private float killDeathRatio;

        public Text OverallText, LastMonthText, LastWeekText;
        public CircularProgressBar OverallProgressBar, LastMonthProgressBar, LastWeekProgressBar;
        private int overall, lastmonth, lastweek;

        public List<ProgressElement> RandomProgressElements;

        void Start()
        {
            GenerateTestData();
            UpdateUI();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GenerateTestData();
                UpdateUI();
            }
        }

        void GenerateTestData()
        {
            PopulateScores();

            PopulateDiagram();

            kills = Random.Range(40, 90);
            deaths = Random.Range(5, 20);
            killDeathRatio = (float)kills / (float)deaths;

            overall = Random.Range(200, 400);
            lastmonth = Random.Range(30, overall - 50);
            lastweek = Random.Range(10, lastmonth - 10);
        }

        void UpdateUI()
        {
            for (int i = 0; i < ScoreProgressBars.Count && ScoreProgressBars.Count == scores.Count && scores.Count == ScoreTextElements.Count; i++)
            {
                ScoreProgressBars[i].Value = (float)scores[i] / (float)scores.Max();
                ScoreTextElements[i].text = scores[i].ToString("N0");
            }

            for (int i = 0; i < DiagramProgressBars.Count && DiagramProgressBars.Count == diagramValues.Count; i++)
            {
                DiagramProgressBars[i].Value = diagramValues[i] / 8f;
            }
            DiagramCaption.text = "Best kill/death ratio last week: " + ((float)diagramValues.Max()).ToString("N2");
            IndicatorText.text = diagramValues[diagramValues.Count - 1].ToString("N2");

            KillsText.text = kills.ToString();
            DeathsText.text = deaths.ToString();
            KillDeathRatioProgressBar.Value = 1 - (float)deaths / (float)kills;
            KillDeathRatioText.text = killDeathRatio.ToString("N2");

            OverallProgressBar.Value = 1f;
            LastMonthProgressBar.Value = (float)lastmonth / (float)overall;
            LastWeekProgressBar.Value = (float)lastweek / (float)overall;
            OverallText.text = overall.ToString() + " OVERALL";
            LastMonthText.text = lastmonth.ToString() + " LAST MONTH";
            LastWeekText.text = lastweek.ToString() + " LAST WEEK";

            for (int i = 0; i < RandomProgressElements.Count; i++)
            {
                RandomProgressElements[i].Value = Random.Range(0f, 1f);
            }
        }

        void PopulateScores()
        {
            scores.Clear();
            for (int i = 0; i < 8; i++)
            {
                scores.Add(Random.Range(500, 2500));
            }
            scores = scores.OrderByDescending(i => i).ToList();
        }

        void PopulateDiagram()
        {
            diagramValues.Clear();
            for (int i = 0; i < 7; i++)
            {
                diagramValues.Add(Random.Range(3f, 8f));
            }
        }
    }
}

