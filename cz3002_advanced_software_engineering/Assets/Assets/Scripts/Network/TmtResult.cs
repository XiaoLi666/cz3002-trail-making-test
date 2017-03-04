using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class TmtResult {
		public static int TYPE_A = 0;
		public static int TYPE_B = 1;
		private string resultId;
		private string nric;
		private string date;
		private double errorRate;
		private double timeTaken;
		private int type;

		public TmtResult(string result_id, string nric, double time_taken, double error_rate, int type) {
			this.resultId = result_id;
			this.nric = nric;
			this.errorRate = error_rate;
			this.timeTaken = time_taken;
			this.type = type;
		}

		public override string ToString() {
			return resultId + "\t" + nric + "\t" + date + "\t" + timeTaken + "\t" + errorRate + "\t" + type;
		}

		public string Date {
			get { return date; }
			set { date = value; }
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
			if (x == "") return null;
			string[] list = x.Split(':');
			TmtResult r = new TmtResult (list[0], list[1], double.Parse(list[5]), double.Parse(list[6]), int.Parse(list[7]));
			r.Date = list[2] + ":" + list[3] + ":"  + list[4].Substring(0,2);
			return r;
		}

		public static List<TmtResult> parseResultList(string x) {
			if (x.Equals("")) {
				return null;
			}

			List<TmtResult> list = new List<TmtResult>();
			string[] temp = x.Split('<');
			foreach(string z in temp) {
				list.Add(parseResult(z));
			}
			return list;
		}
	}
}