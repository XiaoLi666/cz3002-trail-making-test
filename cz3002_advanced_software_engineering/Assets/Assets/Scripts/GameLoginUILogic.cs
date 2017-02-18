using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Network;

namespace UI {
	public class GameLoginUILogic : MonoBehaviour {
#region attributes
		[SerializeField]
		private GameObject m_gameLogin;
		[SerializeField]
		private GameObject m_gameRegistration;
		[SerializeField]
		private Text m_loginNRICInput;
		[SerializeField]
		private Text m_loginPasswordInput;
		[SerializeField]
		private GameObject m_networkManager;
#endregion

#region custom methods
		public void RegisterBtn() {
			m_gameLogin.SetActive (false);
			m_gameRegistration.SetActive (true);
		}

		public void GameLoginOKBtn(int id) {
			// Validate the user information
			string nric = m_loginNRICInput.text;
			string password = m_loginPasswordInput.text;
			m_networkManager.GetComponent<NetworkManager> ().CheckUser (nric, password);
			bool is_auth = m_networkManager.GetComponent<NetworkManager> ().m_isUserAuthenticated;

			if (is_auth == true) {
				SceneManager.LoadScene (id);
			}
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