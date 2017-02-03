using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUILogic : MonoBehaviour {
	public void ReturnToMainMenu (int id) {
		SceneManager.LoadScene (id);
	}
}
