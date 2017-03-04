using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Network;

namespace Game {
	public class PlayerDataCollection {
		#region attributes
		private static PlayerDataCollection m_instance = new PlayerDataCollection();
		private string m_nric = "";
		private string m_password = "";
		private List<TmtResult> m_tmtResult = null;
		private TmtResult m_resultToSave = null;

		public string NRIC {
			set { m_nric = value; }
			get { return m_nric; }
		}

		public string Password {
			set { m_password = value; }
			get { return m_password; }
		}

		public List<TmtResult> TmtResult {
			get { return m_tmtResult; }
			set { m_tmtResult = value; }
		}

		public TmtResult ResultToSave {
			get { return m_resultToSave; }
		}
		#endregion

		#region custom methods
		private PlayerDataCollection() {
			m_tmtResult = new List<TmtResult>();
		}

		public static PlayerDataCollection GetInstance() {
			return m_instance;
		}

		public void ToSaveResult(double error_rate, double time_taken, int type) {
			System.Guid guid = System.Guid.NewGuid();
			m_resultToSave = new TmtResult (guid.ToString (),m_nric,time_taken,error_rate, type);
		}

		public void ClearUserInfo() {
			NRIC = "";
			Password = "";
			m_resultToSave = null;
			m_tmtResult = null;
			m_tmtResult = new List<TmtResult> ();
		}

		public List<PlayerRecordUnit> GetPlayerRecordUnits() {
			List<PlayerRecordUnit> list = new List<PlayerRecordUnit> ();
			for (int i = 0; i < m_tmtResult.Count; ++i) {
				string type = "Type A";
				if (m_tmtResult [i].getType () == 1) {
					type = "Type B";
				}
				list.Add(new PlayerRecordUnit (
					m_tmtResult[i].Date, 
					type, 
					m_tmtResult[i].getTimeTaken().ToString("0.00"), 
					m_tmtResult[i].getErrorRate().ToString("0.00")
				));
			}
			return list;
		}
		#endregion
	}
}
