using UnityEngine;
using Game;
using Network;

namespace Event {
	public class GameNetworkEventHandler : MonoBehaviour {
		#region custom methods
		public void EventSaveResult(TmtResult result) {
			StartCoroutine (HttpManager.GetInstance().SaveResultRequest(result, this));
		}
		public void EventSaveResultCallback(string response_string) {
			if (response_string.Equals ("true")) {
				Debug.Log ("Save result - successful.");
			} else {
				Debug.Log ("Save result - failed.");
			}
		}
		#endregion
	}
}