using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game;
using Network;

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
		[SerializeField]
		private GameObject m_networkManager;
		[SerializeField]
		private GameObject m_infoBox;
		[SerializeField]
		private InputField m_loginViewUserNRICInput;
		[SerializeField]
		private InputField m_loginViewPasswordInput;
		[SerializeField]
		private InputField m_registrationViewNRICInput;
		[SerializeField]
		private InputField m_registrationViewNameInput;
		[SerializeField]
		private InputField m_registrationViewAgeInput;
		[SerializeField]
		private InputField m_registrationViewPasswordInput;
		[SerializeField]
		private InputField m_registrationViewValidatedPasswordInput;
		[SerializeField]
		private Text m_infoBoxTitle;
		[SerializeField]
		private Text m_infoBoxContent;
		[SerializeField]
		private Text m_userInfo;
		private NetworkManager m_networkManagerClass;
		private GameObject m_currentView = null;

		public GameObject m_testing;

#endregion

#region override methods
		public void Start() {
			m_networkManagerClass = m_networkManager.GetComponent<NetworkManager> ();
			if (PlayerDataCollection.GetInstance ().NRIC != "") {
				ShowView (m_selectionView);
				m_userInfo.text = "Welcome " + PlayerDataCollection.GetInstance ().NRIC;
			} else {
				ShowView (m_loginView);
			}


			RectTransform panelRectTransform = m_testing.GetComponent<RectTransform>();
			panelRectTransform.sizeDelta = new Vector2 (2000, panelRectTransform.sizeDelta.y);
		}
#endregion

#region network methods
#endregion

#region utility functions
		public void ShowView(GameObject view) {
			if (m_currentView == view) return;
			if (m_currentView != null) m_currentView.SetActive (false);
			view.SetActive (true);
			m_currentView = view;
		}

		public void ShowInfoBox(string title, string content) {
			m_infoBoxTitle.text = title;
			m_infoBoxContent.text = content;
			m_infoBox.SetActive (true);
		}
#endregion

#region ui methods
		public void ClickOnInfoBoxOkBtn() {
			m_infoBox.SetActive (false);
		}
		// ----------------------------------------------------------------------------------------------------
		public void ClickOnLoginViewOkBtn() {
			m_networkManagerClass.CheckUserRequest (m_loginViewUserNRICInput.text, m_loginViewPasswordInput.text);
		}
		public void ClickOnLoginViewRegisterBtn() {
			ShowView(m_registrationView);
		}
		public void ShowStartGameView () {
			m_userInfo.text = "Welcome " + PlayerDataCollection.GetInstance ().NRIC;
			ShowView(m_startGameView);
			m_networkManagerClass.GetAllResultsRequest (
				PlayerDataCollection.GetInstance ().TmtResult,
				PlayerDataCollection.GetInstance ().NRIC
			);
		}
		// ----------------------------------------------------------------------------------------------------
		public void ClickOnRegistrationViewOkBtn() {
			if (m_registrationViewPasswordInput.text.Equals(m_registrationViewValidatedPasswordInput.text) == false) {
				ShowInfoBox ("Error", "Password validation failed!");
				return;
			}
			m_networkManagerClass.RegistrationRequest (m_registrationViewNRICInput.text, m_registrationViewPasswordInput.text, m_registrationViewNameInput.text, m_registrationViewAgeInput.text);
		}
		public void ClickOnRegistrationViewBackBtn() {
			ShowView (m_loginView);
		}
		public void ShowLoginView(string nric, string password) {
			ShowView (m_loginView);
			m_loginViewUserNRICInput.text = nric;
			m_loginViewPasswordInput.text = password;
		}
		// ----------------------------------------------------------------------------------------------------
		public void ClickOnStartGameViewStartBtn() {
			ShowView(m_selectionView);
		}
		public void ClickOnStartGameViewStatisticsBtn() {
			ShowView(m_statisticView);
		}
		public void ClickOnStartGameViewLogoutBtn() {
			// Validate the logout process
			ShowView(m_loginView);
			m_userInfo.text = "No User";
			m_loginViewUserNRICInput.text = "";
			m_loginViewPasswordInput.text = "";
		}
		// ----------------------------------------------------------------------------------------------------
		public void ClickOnSelectionViewTMTTypeABtn(int id) {
			GameLogic.m_type = GameLogic.TMT_TYPE.TMT_TYPE_A;
			SceneManager.LoadScene (id);
		}
		public void ClickOnSelectionViewTMTTypeBBtn(int id) {
			GameLogic.m_type = GameLogic.TMT_TYPE.TMT_TYPE_B;
			SceneManager.LoadScene (id);
		}
		public void ClickOnSelectionViewBackBtn() {
			ShowView(m_startGameView);
		}
		// ----------------------------------------------------------------------------------------------------
		public void ClickOnStatisticsViewBackBtn() {
			ShowView(m_startGameView);
		}

#endregion
	}
}