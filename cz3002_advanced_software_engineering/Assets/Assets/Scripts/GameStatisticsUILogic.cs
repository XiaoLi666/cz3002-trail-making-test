using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
	public class GameStatisticsUILogic : MonoBehaviour {
#region attributes
		// TODO:
#endregion

#region custom methods
		public void BackBtn(int id) {
			SceneManager.LoadScene (id);
		}
#endregion
	}
}