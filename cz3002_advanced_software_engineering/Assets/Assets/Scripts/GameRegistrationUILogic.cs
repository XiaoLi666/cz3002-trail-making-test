using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
	public class GameRegistrationUILogic : MonoBehaviour {
#region attributes
		[SerializeField]
		private GameObject m_loginUI;
		[SerializeField]
		private GameObject m_registerUI;
#endregion

#region custom methods
		public void OKBtn() {
			// Add registration logic here
		}

		public void BackBtn() {
			m_registerUI.SetActive (false);
			m_loginUI.SetActive (true);
		}
#endregion
	}
}