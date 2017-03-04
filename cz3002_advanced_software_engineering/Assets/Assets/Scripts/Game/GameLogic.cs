using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;
using Network;

namespace Game {
	public class GameLogic : MonoBehaviour {
		#region attributes
		[SerializeField] private GameObject m_circlePrefab = null;
		[SerializeField] private GameObject m_linePrefab = null;
		[SerializeField] private GameObject m_resultPanel = null;
		[SerializeField] private Text m_resultValue = null;
		[SerializeField] private Vector2 m_gameAreaMin;
		[SerializeField] private Vector2 m_gameAreaMax;
		[SerializeField] private int m_circleCounter; // normally, this counter should be 25
		[SerializeField] private float m_circleRadius;

		private GameObject m_prefabLine;
		private GameObject m_errorCircle = null;
		private Vector2 m_previousPosition = Vector2.zero;
		private List<GameObject> m_circleList = new List<GameObject>();
		private List<GameObject> toura = new List<GameObject>();
		private List<GameObject> m_lineSegments = new List<GameObject>();
		private string[] m_tmtTypeAList = {"1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25"}; // But the calculation will be based on the circle counter
		private string[] m_tmtTypeBList = {"1","A","2","B","3","C","4","D","5","E","6","F","7","G","8","H","9","I","10","J","11","K","12","L","13"};
		private bool m_complete = false;
		private bool m_startTimer = false;
		private bool m_isInitialized = false;
		private bool m_alreadyWrong = false;
		private double m_wrongCount = 0;
		private double m_timer = 0.0f;
		private int m_tapIndex = 0;
		private const float VERY_BIG_DIST = 10000.0f;

		static public TMT_TYPE m_type = TMT_TYPE.TMT_TYPE_A;
		public enum TMT_TYPE {
			TMT_TYPE_A,
			TMT_TYPE_B
		}
		#endregion

		#region override methods
		void Start () {
			// need to random generate a lot of circles on the game scene
			GameObject prefab_to_create = null;
			for (int i = 0; i < m_circleCounter; ++i)  {
				Vector3 random_position = GenerateRandomPosition ();
				if (IsPositionOverlapped (random_position)) {
					--i;
					continue;
				}
				prefab_to_create = Instantiate (m_circlePrefab, random_position, m_circlePrefab.transform.rotation) as GameObject;
				m_circleList.Add (prefab_to_create);
			}
			GenerateSequence();
			m_isInitialized = true;
		}
		
		// Update is called once per frame
		void Update () {
			if (m_isInitialized == false) {
				return;
			}

			if (m_complete == false && m_startTimer == true) 
				m_timer += Time.deltaTime;

			if (Input.touchCount > 0) {
				// Going to check the user input here:
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					m_previousPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
					m_alreadyWrong = false;
					m_errorCircle = null;
				}

				if (Input.GetTouch (0).phase == TouchPhase.Moved) {
					m_prefabLine = Instantiate (m_linePrefab, Vector3.zero, m_linePrefab.transform.rotation) as GameObject; // this is the thing that is always being created
					LineRenderer line_renderer = m_prefabLine.GetComponent<LineRenderer>();
					Vector2 position_touched = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

					// Setting line renderer
					line_renderer.SetPosition (0, m_previousPosition);
					line_renderer.SetPosition (1, position_touched);
					line_renderer.startWidth = 0.04f;
					line_renderer.endWidth = 0.04f;

					m_previousPosition = position_touched;
					m_lineSegments.Add (m_prefabLine);

					for (int c_idx = 0; c_idx < m_circleList.Count; ++ c_idx) {
						float distance = Vector2.Distance (m_circleList[c_idx].transform.position, position_touched);
						if (distance < m_circleRadius) {
							HighlightCircle(m_circleList[c_idx]);
							break;
						}
					}
				}
			}

			if (m_tapIndex == m_circleCounter) {
				m_resultPanel.SetActive (true);
				m_resultValue.text = m_timer.ToString("0.00");
				m_complete = true;
				int type = m_type == TMT_TYPE.TMT_TYPE_A ? 0 : 1;
				double error_rate = m_wrongCount/(m_wrongCount+25);
				PlayerDataCollection.GetInstance ().ToSaveResult(m_wrongCount, m_timer, type);
			}

			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
				for (int i = 0; i < m_lineSegments.Count; ++ i) {
					UnityEngine.Object.DestroyImmediate (m_lineSegments [i]);
				}
			}
		}
		#endregion

		#region custom methods
		private Vector3 GenerateRandomPosition() {
			return new Vector3 (UnityEngine.Random.Range(m_gameAreaMin.x,m_gameAreaMax.x), UnityEngine.Random.Range(m_gameAreaMin.y,m_gameAreaMax.y), 0.0f);
		}

		// Simple tradeoff algorithm, can handle small number of circles
		private bool IsPositionOverlapped(Vector3 position) {
			foreach (GameObject circle in m_circleList) {
				if (Vector3.Distance (circle.transform.position, position) <= m_circleRadius * 2) {
					return true;
				}
			}
			return false;
		}

		private void SetTextForCircle(GameObject circle, string text) {
			TextMesh tm = circle.GetComponentInChildren<TextMesh>();
			tm.text = text;
		}

		// Using rubber-band algorithm
		private void GenerateSequence() {
			toura.Add (m_circleList[0]);
			toura.Add (m_circleList[1]);
			List<GameObject> rest = m_circleList.GetRange(2, m_circleList.Count-2);
			List<GameObject> rotated_toura = new List<GameObject>();
			List<float> dist = new List<float> ();

			foreach (GameObject next in rest) {
				rotated_toura.Clear();
				for (int i = 1; i < toura.Count; ++ i) {
					rotated_toura.Add (toura[i]);
				}
				rotated_toura.Add (toura[0]);

				dist.Clear ();
				// Algorithm core
				for (int i = 0; i < toura.Count; ++i) {
					GameObject first_obj = toura [i];
					GameObject second_obj = rotated_toura[i];
					float dist_fn = Vector2.Distance(first_obj.transform.position, next.transform.position);
					float dist_ns = Vector2.Distance(next.transform.position, second_obj.transform.position);
					float dist_fs = Vector2.Distance(first_obj.transform.position, second_obj.transform.position);
					float cost = dist_fn + dist_ns - dist_fs;
					dist.Add (cost);
				}

				// Find the index of shortest dist
				int shortest_dist_index = 0;
				float shortest_dist = VERY_BIG_DIST;
				for (int i = 0; i < dist.Count; ++ i) {
					if (dist[i] < shortest_dist) {
						shortest_dist = dist [i];
						shortest_dist_index = i;
					}
				}

				List<GameObject> new_toura = new List<GameObject>();
				for (int i = 0; i < toura.Count; ++ i) {
					new_toura.Add (toura[i]);
					if (i == shortest_dist_index) {
						new_toura.Add (next);
					}
				}
				toura = new_toura;
			}

			int label = 0;
			foreach (GameObject obj in toura) {
				switch (m_type) {
				case TMT_TYPE.TMT_TYPE_A:
					SetTextForCircle (obj, m_tmtTypeAList[label].ToString());
					break;
				case TMT_TYPE.TMT_TYPE_B:
					SetTextForCircle (obj, m_tmtTypeBList[label].ToString());
					break;
				}
				label++;
			}
		}

		private void HighlightCircle(GameObject circle) {
			if (circle == toura [m_tapIndex]) {
				if (m_alreadyWrong == false) {
					if (m_tapIndex == 0)
						m_startTimer = true;
					circle.GetComponent<Renderer> ().material.color = new Color (130.0f, 130.0f, 0.0f, 1.0f);
					++m_tapIndex;
					m_lineSegments.Clear ();
				}
			} else if (m_tapIndex > 0 && circle != toura [m_tapIndex-1] && circle != m_errorCircle) {
				m_alreadyWrong = true;
				++ m_wrongCount;
				m_errorCircle = circle;
			}
		}
		#endregion
	}
}