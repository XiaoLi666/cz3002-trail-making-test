using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

namespace Network {
	public class TmtResult {
		public static int TYPE_A = 0;
		public static int TYPE_B = 1;
		private string resultId;
		private string nric;
		private DateTime date;
		private double errorRate;
		private double timeTaken;
		private int type;

		public TmtResult(string resultId,string nric,DateTime date,double timeTaken,double errorRate,int type) {
			this.resultId = resultId;
			this.nric = nric;
			this.date = date;
			this.errorRate = errorRate;
			this.timeTaken = timeTaken;
			this.type = type;
		}

		public override string ToString() {
			return resultId + "\t" + nric + "\t" + date + "\t" + timeTaken + "\t" + errorRate + "\t" + type;
		}
		public void setResultId(string resultId) {
			this.resultId = resultId;
		}
		public string getResultId() {
			return resultId;
		}
		public void setNric(string nric) {
			this.nric = nric;
		}
		public string getNric() {
			return nric;
		}
		public void setDate(DateTime date) {
			this.date = date;
		}
		public DateTime getDate() {
			return date;
		}
		public void setErrorRate(double errorRate) {
			this.errorRate = errorRate;

		}
		public double getErrorRate() {
			return errorRate;
		}
		public void setTimeTaken(double timeTaken) {
			this.timeTaken = timeTaken;
		}
		public double getTimeTaken() {
			return timeTaken;
		}
		public void setType(int type) {
			this.type = type;
		}

		public int getType() {
			return type;
		}

		public static TmtResult parseResult(string x) {
			string[] list = x.Split(':');
			return new TmtResult(list[0],list[1],parseDateTime(list[2]), Double.Parse(list[3]),Double.Parse(list[4]),int.Parse(list[5]));
		}

		public static ArrayList parseResultList(string x) {
			ArrayList list = new ArrayList();
			string[] temp = x.Split('<');
			foreach(string z in temp) {
				list.Add(parseResult(z));
			}
			return list;

		}

		public static DateTime parseDateTime(string dateString) {
			Console.WriteLine("Input string " + dateString);
			return new DateTime(long.Parse(dateString));
		}
	}
}