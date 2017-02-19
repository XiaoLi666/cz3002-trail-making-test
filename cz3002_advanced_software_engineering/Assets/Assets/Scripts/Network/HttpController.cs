using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Network {
	public class HttpController {
		String url = "http://localhost:8080/tmtServer/HttpController";
		/*
		public Boolean isUserAuthenticated(String nric,String password) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Headers["function"]="userauth";
			request.Headers["nric"] = nric;
			request.Headers["password"] = password;
			HttpWebResponse response = (HttpWebResponse) request.GetResponse();
			String responseString=response.Headers["responseString"];
			Console.WriteLine("ResStr "+responseString);
			if (responseString.Equals("true"))
				return true;
			return false;
		}
		public Boolean register(User user) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Headers["function"] = "registeruser";
			request.Headers["nric"] = user.getNric();
			request.Headers["password"] = user.getPassword();
			request.Headers["age"] = user.getAge().ToString();
			request.Headers["name"] = user.getName();
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			String responseString = response.Headers["responseString"];
			Console.WriteLine("ResStr " + responseString);
			if (responseString.Equals("true"))
				return true;
			return true;
		}

		public void saveResult(TmtResult result) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Headers["function"] = "saveres";
			request.Headers["resultId"] = result.getResultId();
			request.Headers["nric"] = result.getNric();
			request.Headers["timeTaken"] = result.getTimeTaken().ToString();
			request.Headers["errorRate"] = result.getErrorRate().ToString();
			request.Headers["date"] = result.getDate().ToLongTimeString();
			request.Headers["type"] = result.getType().ToString();
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			String responseString = response.Headers["responseString"];
			Console.WriteLine("ResStr " + responseString);
		}

		public ArrayList getAllResults(String nric) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Headers["function"] = "allres";
			request.Headers["nric"] = nric;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			String responseString = response.Headers["responseString"];
			Console.WriteLine("ResStr " + responseString);
			return TmtResult.parseResultList(responseString);



		}
		/*
		public TmtResult getSingleResult(String resultId) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Headers["function"] = "singleres";
			request.Headers["resultId"] = resultId;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			String responseString = response.Headers["responseString"];
			Console.WriteLine("ResStr " + responseString);

			return TmtResult.parseResult(responseString);
		}
		public TmtResult getLatestResult(String nric) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Headers["function"] = "latestres";
			request.Headers["nric"] = nric;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			String responseString = response.Headers["responseString"];
			Console.WriteLine("ResStr " + responseString);
			return TmtResult.parseResult(responseString);
		}
		*/
	}
}