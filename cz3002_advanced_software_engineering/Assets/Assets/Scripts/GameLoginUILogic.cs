using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
	public class GameLoginUILogic : MonoBehaviour {
#region attributes
		[SerializeField]
		private GameObject m_gameLogin;
		[SerializeField]
		private GameObject m_gameRegistration;
#endregion

#region custom methods
		public void RegisterBtn() {
			m_gameLogin.SetActive (false);
			m_gameRegistration.SetActive (true);
		}

		public void GameLoginOKBtn(int id) {
			// Validate the user information
			SceneManager.LoadScene (id);
		}

		public void GameRegistrationOKBtn() {
			// Add registration logic here

			BackBtn ();
		}

		public void BackBtn() {
			m_gameRegistration.SetActive (false);
			m_gameLogin.SetActive (true);
		}
#endregion
	}
}