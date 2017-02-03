using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;

namespace UI {
	public class UILogic : MonoBehaviour {
		#region attributes
		[SerializeField]
		private GameObject m_loginUI;
		[SerializeField]
		private GameObject m_registerUI;
		[SerializeField]
		private GameObject m_selectionUI;
		#endregion

#region custom methods
		public void LoadTMTTypeAById (int id) {
			GameLogic.m_type = GameLogic.TMT_TYPE.TMT_TYPE_A;
			SceneManager.LoadScene (id);
		}

		public void LoadTMTTypeBlById (int id) {
			GameLogic.m_type = GameLogic.TMT_TYPE.TMT_TYPE_B;
			SceneManager.LoadScene (id);
		}

		public void LoginUIRegisterBtn() {
			// Disable Login UI

			// Enable Register UI
		}

		public void LoginUIOKBtn() {
		}

		public void RegisterUIOKBtn() {
		}

		public void RegisterUICancelBtn() {
			// Disable Register UI

			// Enable Login UI
		}
#endregion
	}
}
