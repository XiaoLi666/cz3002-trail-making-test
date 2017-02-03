using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Game {
	public class GameLogic : MonoBehaviour {
#region attributes
		[SerializeField]
		private GameObject m_circlePrefab;
		[SerializeField]
		private GameObject m_linePrefab;
		[SerializeField]
		private Vector2 m_gameAreaMin;
		[SerializeField]
		private Vector2 m_gameAreaMax;
		[SerializeField]
		private int m_circleCounter; // normally, this counter should be 25
		[SerializeField]
		private float m_circleRadius;

		private bool m_isInitialized = false;
		private int m_tapIndex = 0;
		private const float VERY_BIG_DIST = 10000.0f;
		private List<GameObject> m_circleList = new List<GameObject>();
		private List<GameObject> toura = new List<GameObject>();
		private LinkedList<GameObject> m_circleLinkedList = new LinkedList<GameObject>(); // for generation use only
		private string[] m_tmtTypeAList = {"1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25"}; // But the calculation will be based on the circle counter
		private string[] m_tmtTypeBList = {"1","A","2","B","3","C","4","D","5","E","6","F","7","G","8","H","9","I","10","J","11","K","12","L","13"};

		static public TMT_TYPE m_type = TMT_TYPE.TMT_TYPE_A;
		public enum TMT_TYPE {
			TMT_TYPE_A,
			TMT_TYPE_B
		}
#endregion

#region override methods
		// Use this for initialization
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
				m_circleLinkedList.AddLast(prefab_to_create);
			}
			GenerateSequence();
			m_isInitialized = true;
		}
		
		// Update is called once per frame
		void Update () {
			if (m_isInitialized == false) {
				return;
			}

			// Going to check the user input here:
			for (var i = 0; i < Input.touchCount; ++i) {
				if (Input.GetTouch(i).phase == TouchPhase.Began) {
					Vector2 position_touched = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
					foreach (GameObject circle in m_circleList) {
						float distance = Vector2.Distance (circle.transform.position, position_touched);
						if (distance < m_circleRadius) {
							Analysis(circle);
							break;
						}
					}
				}
			}
		}
#endregion

#region custom methods
		private Vector3 GenerateRandomPosition() {
			return new Vector3 (Random.Range(m_gameAreaMin.x,m_gameAreaMax.x), Random.Range(m_gameAreaMin.y,m_gameAreaMax.y), 0.0f);
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

		private GameObject GetLinkedListItemByIndex(LinkedList<GameObject> ll, int index) {
			int count = 0;
			foreach (GameObject i in ll) {
				if (count == index) {
					return i;
				}
				count++;
			}
			return null;
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

		private void Analysis(GameObject circle) {
			if (circle == toura[m_tapIndex]) {
				if (m_tapIndex >= 1) {
					GameObject prefab_line = Instantiate (m_linePrefab, Vector3.zero, m_linePrefab.transform.rotation) as GameObject;
					LineRenderer line_renderer = prefab_line.GetComponent<LineRenderer>();
					line_renderer.SetPosition (0, toura [m_tapIndex - 1].transform.position);
					line_renderer.SetPosition (1, toura [m_tapIndex].transform.position);
					line_renderer.startWidth = 0.1f;
					line_renderer.endWidth = 0.1f;
				}
				circle.GetComponent<Renderer>().material.color = new Color (130.0f,130.0f,0.0f,1.0f);
				++m_tapIndex;
			}
		}
#endregion
	}
}