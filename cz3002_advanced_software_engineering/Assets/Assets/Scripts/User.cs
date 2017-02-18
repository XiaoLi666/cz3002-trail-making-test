using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

namespace Network {
	public class User {
		private String nric;
		private String password;
		private String name;
		private int age;
		public User(String nric,String password,String name,int age) {
			this.nric = nric;
			this.password = password;
			this.name = name;
			this.age = age;
		}
		public override string ToString() {
			return nric + "\t" + password + "\t" + name + "\t" + age;
		}
		public void setNric(String nric) {
			this.nric = nric;
		}
		public String getNric() {
			return nric;
		}
		public void setPassword(String password) {
			this.password = password;
		}
		public String getPassword() {
			return password;
		}
		public void setName(String name) {

			this.name = name;
		}
		public String getName() {
			return name;
		}

		public void setAge(int age) {
			this.age = age;
		}

		public int getAge() {
			return age;
		}
	}
}