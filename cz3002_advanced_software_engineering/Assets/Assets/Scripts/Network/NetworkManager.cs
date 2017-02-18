using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Network {
	public class NetworkManager : MonoBehaviour {
		private string m_url = "http://172.20.207.172:8080/tmtServer/HttpController";
		public bool m_isUserAuthenticated = false;

		// Use this for initialization
		void Start () {
			
		}

		public void CheckUser(string nric, string password) {
			StartCoroutine (IsUserAuthenticated(nric, password));
		}

		private IEnumerator IsUserAuthenticated(string nric, string password) {
			m_isUserAuthenticated = false;
			List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
			UnityWebRequest request = UnityWebRequest.Post(m_url, formData);
			request.SetRequestHeader ("function", "userauth");
			request.SetRequestHeader ("nric", nric);
			request.SetRequestHeader ("password", password);
			yield return request.Send ();

			if(request.isError) {
				Debug.Log(request.error);
			}
			else {
				Debug.Log("Form upload complete!");
				Dictionary<string, string> response = request.GetResponseHeaders();
				string response_string = response["responseString"];
				if (response_string.Equals ("true")) {
					m_isUserAuthenticated = true;
				}
			}
		}
	}
}