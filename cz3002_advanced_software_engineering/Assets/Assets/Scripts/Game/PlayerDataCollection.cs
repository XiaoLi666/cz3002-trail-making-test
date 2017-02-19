using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class PlayerDataCollection {
#region attributes
		private static PlayerDataCollection m_instance = new PlayerDataCollection();
		private string m_nric = "";
		private string m_password = "";
		private ArrayList m_tmtResult = null;

		public string NRIC {
			set {
				m_nric = value;
			}
			get {
				return m_nric;
			}
		}

		public string Password {
			set {
				m_password = value;
			}
			get {
				return m_password;
			}
		}

		public ArrayList TmtResult {
			get {
				return m_tmtResult;
			}
		}

#endregion

#region custom methods
		private PlayerDataCollection() {
		}

		public static PlayerDataCollection GetInstance() {
			return m_instance;
		}

		public void ClearUserInfo() {
			NRIC = "";
			Password = "";
		}
#endregion
	}
}
