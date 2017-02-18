using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
	public class HUDManager : MonoBehaviour {


#region custom methods
		public void ClickOnHUDBackBtn(int id) {
			SceneManager.LoadScene (id);
		}

		public void ClickOnResultPanelOkBtn(int id) {
			SceneManager.LoadScene (id);
		}
#endregion
	}
}