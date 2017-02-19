using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UI;
using Game;

namespace Network {
	public class NetworkManager : MonoBehaviour {
#region attributes
		[SerializeField]
		private GameObject m_UIManager;
		private UIManager m_UIManagerClass;
		private string m_url = "http://172.20.207.172:8080/tmtServer/HttpController";
#endregion

#region override methods
		void Start() {
			m_UIManagerClass = m_UIManager.GetComponent<UIManager> ();
		}
#endregion

#region coroutine methods
		public void CheckUserRequest (string nric, string password) {
			StartCoroutine (CheckUser(nric, password));
		}

		public void RegistrationRequest (string nric, string password, string name, string age) {
			StartCoroutine (Registration(nric, password, name, age));
		}

		public void GetAllResultsRequest (ArrayList tmt_result, string nric) {
			StartCoroutine (GetAllResults (tmt_result, nric));
		}
#endregion

#region custom methods
		private IEnumerator GetAllResults(ArrayList tmt_result, string nric) {
			List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
			UnityWebRequest request = UnityWebRequest.Post(m_url, formData);
			request.SetRequestHeader ("function", "allres");
			request.SetRequestHeader ("nric", nric);
			yield return request.Send ();

			if (request.isError) {
				Debug.Log (request.error);
			} else {
				Dictionary<string, string> response = request.GetResponseHeaders ();
				string response_string = response ["responseString"];
				tmt_result = TmtResult.parseResultList (response_string);
			}
		}

		private IEnumerator CheckUser(string nric, string password) {
			List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
			UnityWebRequest request = UnityWebRequest.Post(m_url, formData);
			request.SetRequestHeader ("function", "userauth");
			request.SetRequestHeader ("nric", nric);
			request.SetRequestHeader ("password", password);
			yield return request.Send ();

			if(request.isError) {
				Debug.Log(request.error);
			} else {
				Dictionary<string, string> response = request.GetResponseHeaders();
				string response_string = response["responseString"];
				if (response_string.Equals ("true")) {
					PlayerDataCollection.GetInstance ().NRIC = nric;
					PlayerDataCollection.GetInstance ().Password = password;
					m_UIManagerClass.ShowStartGameView ();
				} else {
					m_UIManagerClass.ShowInfoBox ("Alert", "Invalid NRIC or Password!");
				}
			}
		}

		private IEnumerator Registration(string nric, string password, string name, string age) {
			List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
			UnityWebRequest request = UnityWebRequest.Post(m_url, formData);
			request.SetRequestHeader ("function", "registeruser");
			request.SetRequestHeader ("nric", nric);
			request.SetRequestHeader ("password", password);
			request.SetRequestHeader ("name", name);
			request.SetRequestHeader ("age", age);
			yield return request.Send ();

			if (request.isError) {
				Debug.Log (request.error);
			} else {
				Dictionary<string, string> response = request.GetResponseHeaders();
				string response_string = response["responseString"];
				if (response_string.Equals ("true")) {
					m_UIManagerClass.ShowLoginView (nric, password);
				} else {
					m_UIManagerClass.ShowInfoBox ("Error", "Registration failed! (NRIC already registered?)");
				}
			}
		}
#endregion
	}
}