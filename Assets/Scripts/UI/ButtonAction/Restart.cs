using UnityEngine;

public class Restart : MonoBehaviour {

	private GameManager gm;

	void Start () {
		gm = FindObjectOfType<GameManager> ();
	}

	public void RestartGame () {
		gm.RestartGame ();
	}

}
