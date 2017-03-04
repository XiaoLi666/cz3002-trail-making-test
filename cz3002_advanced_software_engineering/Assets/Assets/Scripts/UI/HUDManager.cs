using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Network;
using Event;
using Game;

namespace UI {
	public class HUDManager : MonoBehaviour {
		#region attributes
		[SerializeField]
		private GameObject m_networkEventHandler;
		private GameNetworkEventHandler m_networkEventHandlerClass;
		#endregion

		#region override methods
		void Start() {
			m_networkEventHandlerClass = m_networkEventHandler.GetComponent<GameNetworkEventHandler> ();
		}
		#endregion

		#region custom methods
		public void ClickOnHUDBackBtn(int id) {
			SceneManager.LoadScene (id);
		}

		public void ClickOnResultPanelOkBtn(int id) {
			m_networkEventHandlerClass.EventSaveResult (PlayerDataCollection.GetInstance().ResultToSave);
			SceneManager.LoadScene (id);
		}
		#endregion
	}
}