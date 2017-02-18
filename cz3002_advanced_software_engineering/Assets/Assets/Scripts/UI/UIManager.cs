using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;

namespace UI {
	public class UIManager : MonoBehaviour {
#region attributes
		[SerializeField]
		private GameObject m_loginView;
		[SerializeField]
		private GameObject m_registrationView;
		[SerializeField]
		private GameObject m_startGameView;
		[SerializeField]
		private GameObject m_selectionView;
		[SerializeField]
		private GameObject m_statisticView;
#endregion

#region override methods
		public void Start() {
			Debug.Log ("UI Manager Start Function!");
			// TODO: in this function will check whether the user has already connected to the server
		}
#endregion

#region custom methods
		public void ClickOnLoginViewOkBtn() {
			// Validate user id and password

			m_loginView.SetActive (false);
			m_startGameView.SetActive (true);
		}

		public void ClickOnLoginViewRegisterBtn() {
			m_loginView.SetActive (false);
			m_registrationView.SetActive (true);
		}

		public void ClickOnRegistrationViewOkBtn() {
			// Validate the registration process

			m_registrationView.SetActive (false);
			m_loginView.SetActive (true);
		}

		public void ClickOnRegistrationViewBackBtn() {
			m_registrationView.SetActive (false);
			m_loginView.SetActive (true);
		}

		public void ClickOnStartGameViewStartBtn() {
			m_startGameView.SetActive (false);
			m_selectionView.SetActive (true);
		}

		public void ClickOnStartGameViewStatisticsBtn() {
			m_startGameView.SetActive (false);
			m_statisticView.SetActive (true);
		}

		public void ClickOnStartGameViewLogoutBtn() {
			// Validate the logout process

			m_startGameView.SetActive (false);
			m_loginView.SetActive (true);
		}

		public void ClickOnSelectionViewTMTTypeABtn(int id) {
			GameLogic.m_type = GameLogic.TMT_TYPE.TMT_TYPE_A;
			SceneManager.LoadScene (id);
		}

		public void ClickOnSelectionViewTMTTypeBBtn(int id) {
			GameLogic.m_type = GameLogic.TMT_TYPE.TMT_TYPE_B;
			SceneManager.LoadScene (id);
		}

		public void ClickOnSelectionViewBackBtn() {
			m_selectionView.SetActive (false);
			m_startGameView.SetActive (true);
		}

		public void ClickOnStatisticsViewBackBtn() {
			m_statisticView.SetActive (false);
			m_startGameView.SetActive (true);
		}

#endregion
	}
}