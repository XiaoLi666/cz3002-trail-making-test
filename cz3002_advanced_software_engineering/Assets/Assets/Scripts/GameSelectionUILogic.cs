using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;

namespace UI {
	public class GameSelectionUILogic : MonoBehaviour {
		[SerializeField]
		private GameObject m_startGame;
		[SerializeField]
		private GameObject m_gameSelection;

		// TODO: statistic scene

#region custom methods
		public void StartGameBtn() {
			m_startGame.SetActive (false);
			m_gameSelection.SetActive (true);
		}

		public void ShowStatistic(int id) { // enter statistic scene
			SceneManager.LoadScene (id);
		}

		public void LoadTMTTypeAById (int id) {
			GameLogic.m_type = GameLogic.TMT_TYPE.TMT_TYPE_A;
			SceneManager.LoadScene (id);
		}

		public void LoadTMTTypeBlById (int id) {
			GameLogic.m_type = GameLogic.TMT_TYPE.TMT_TYPE_B;
			SceneManager.LoadScene (id);
		}

		public void Logout(int id) {
			// process logout logic here

			// go back to log in UI
			SceneManager.LoadScene (id);
		}

		public void Back() {
			m_startGame.SetActive (true);
			m_gameSelection.SetActive (false);
		}
#endregion
	}
}
