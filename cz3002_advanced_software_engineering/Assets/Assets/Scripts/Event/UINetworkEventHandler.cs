using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Network;
using Game;

namespace Event {
	public class UINetworkEventHandler : MonoBehaviour {
		#region attributes
		[SerializeField] private GameObject m_UIManager = null;
		private UIManager m_UIManagerClass;
		#endregion

		#region override methods
		void Start () {
			m_UIManagerClass = m_UIManager.GetComponent<UIManager> ();
		}
		#endregion

		#region custom methods
		public void EventRegistration(string nric, string password, string name, string age) {
			StartCoroutine (HttpManager.GetInstance().RegistrationRequest(nric, password, name, age, this));
		}
		public void EventRegistrationCallback(string response_string, string nric, string password) {
			if (response_string.Equals ("true")) {
				m_UIManagerClass.ShowLoginView (nric, password);
			} else { // Registration failed!
			}
		}

		// ----------------------------------------------------------------------------------------------------
		public void EventVerifyUser(string nric, string password) {
			StartCoroutine (HttpManager.GetInstance().VerifyUserRequest(nric, password, this));
		}
		public void EventVerifyUserCallback(string response_string, string nric, string password) {
			if (response_string.Equals ("true")) {
				PlayerDataCollection.GetInstance ().NRIC = nric;
				PlayerDataCollection.GetInstance ().Password = password;
				m_UIManagerClass.ShowStartGameView ();
			} else {
				m_UIManagerClass.ShowInfoBox ("Alert", "Invalid NRIC or Password!");
			}
		}

		// ----------------------------------------------------------------------------------------------------
		public void EventGetAllResult(string nric) {
			StartCoroutine (HttpManager.GetInstance().GetAllResultRequest(nric, this));
		}
		public void EventGetAllResultCallback(List<TmtResult> tmt_result) {
			PlayerDataCollection.GetInstance().TmtResult = tmt_result;
		}
		#endregion
	}
}