using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;

namespace UI {
	public class GameSelectionUILogic : MonoBehaviour {
#region custom methods
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
#endregion
	}
}
