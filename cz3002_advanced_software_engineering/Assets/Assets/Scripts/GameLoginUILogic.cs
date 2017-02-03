using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
	public class GameLoginUILogic : MonoBehaviour {
#region attributes
		[SerializeField]
		private GameObject m_registerUI;
		[SerializeField]
		private GameObject m_loginUI;
		[SerializeField]
		private GameObject m_selectionUI;
#endregion

#region custom methods
		public void RegisterBtn() {
			m_loginUI.SetActive (false);
			m_registerUI.SetActive (true);
		}

		public void OKBtn() {
			// Validate the user information

			// After that
			m_loginUI.SetActive (false);
			m_selectionUI.SetActive (true);
		}

#endregion
	}
}