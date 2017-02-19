using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
	public class InGameUILogic : MonoBehaviour {
		public void ReturnToMainMenu (int id) {
			SceneManager.LoadScene (id);
		}

		public void ResultPanelOk (int id) {
			SceneManager.LoadScene (id);
		}
	}
}