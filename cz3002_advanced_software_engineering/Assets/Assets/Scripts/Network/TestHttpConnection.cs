using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Network;

class TestHttpConnection: MonoBehaviour {
	public GameObject m_testingText;

	void Start() {
		StartCoroutine(TestingFunction());
	}
	/*
	IEnumerator GetText() {
		UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/tmtServer/HttpController");
		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			// Show results as text
			Debug.Log(www.downloadHandler.text);

			// Or retrieve results as binary data
			byte[] results = www.downloadHandler.data;
		}
	}

	IEnumerator Upload() {
		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		formData.Add( new MultipartFormDataSection("field1=foo&field2=bar") );
		formData.Add( new MultipartFormFileSection("my file data", "myfile.txt") );

		UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", formData);
		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Form upload complete!");
		}
	}*/

	IEnumerator TestingFunction() {
		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		UnityWebRequest request = UnityWebRequest.Post("http://172.20.207.172:8080/tmtServer/HttpController", formData);
		request.SetRequestHeader ("function","allres");
		request.SetRequestHeader ("nric","s4134655E");

		yield return request.Send ();

		if(request.isError) {
			Debug.Log(request.error);
		}
		else {
			Debug.Log("Form upload complete!");
			Dictionary<string, string> response = request.GetResponseHeaders();

			string responseString = response["responseString"];
			ArrayList al = TmtResult.parseResultList (responseString);
		}
	}
}