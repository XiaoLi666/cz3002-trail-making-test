using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Event;
using Game;

namespace Network {
	public class HttpManager {
#region attributes
		private static HttpManager m_instance = new HttpManager();
		private string m_url = "http://172.22.210.91:8080/tmtServerV3/HttpController";
#endregion

#region custom methods
		private HttpManager() {}

		public IEnumerator RegistrationRequest(string nric, string password, string name, string age, UINetworkEventHandler handler) {
			List<IMultipartFormSection> form_data = new List<IMultipartFormSection>();
			UnityWebRequest request = UnityWebRequest.Post(m_url, form_data);
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
				handler.EventRegistrationCallback (response_string, nric, password);
			}
		}

		public IEnumerator VerifyUserRequest(string nric, string password, UINetworkEventHandler handler) {
			List<IMultipartFormSection> form_data = new List<IMultipartFormSection>();
			UnityWebRequest request = UnityWebRequest.Post(m_url, form_data);
			request.SetRequestHeader ("function", "userauth");
			request.SetRequestHeader ("nric", nric);
			request.SetRequestHeader ("password", password);
			yield return request.Send ();

			if(request.isError) {
				Debug.Log(request.error);
			} else {
				Dictionary<string, string> response = request.GetResponseHeaders();
				string response_string = response["responseString"];
				handler.EventVerifyUserCallback (response_string, nric, password);
			}
		}

		public IEnumerator SaveResultRequest(TmtResult result, GameNetworkEventHandler handler) {
			List<IMultipartFormSection> form_data = new List<IMultipartFormSection>();
			UnityWebRequest request = UnityWebRequest.Post(m_url, form_data);
			request.SetRequestHeader ("function", "saveres");
			request.SetRequestHeader ("resultId", result.getResultId());
			request.SetRequestHeader ("nric", result.getNric());
			request.SetRequestHeader ("timeTaken", result.getTimeTaken().ToString());
			request.SetRequestHeader ("errorRate", result.getErrorRate().ToString());
			request.SetRequestHeader ("type", result.getType().ToString());
			yield return request.Send ();

			if (request.isError) {
				Debug.Log (request.error);
			} else {
				Dictionary<string, string> response = request.GetResponseHeaders ();
				string response_string = response ["responseString"];
				handler.EventSaveResultCallback (response_string);
			}
		}

		public IEnumerator GetAllResultRequest(string nric, UINetworkEventHandler handler) {
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
				List<TmtResult> tmt_result = TmtResult.parseResultList (response_string);
				handler.EventGetAllResultCallback (tmt_result);
			}
		}

		public static HttpManager GetInstance() {
			return m_instance;
		}
#endregion
	}
}