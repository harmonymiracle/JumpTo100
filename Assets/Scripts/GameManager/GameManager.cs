using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject deathArea;
	public GameSettings gameSettings;
	public int currentLevel;
	public 

	void Start () {
		
	}
	
	
	void Update () {
		
	}

	public void RestartGame () {
		print ("Game Restart");
	}

	public void PauseGame (bool pause) {
		if (pause) {
			Time.timeScale = 0f;

			#if UNITY_EDITOR_64
			print (" Pause!");
			#endif

		} else {
			Time.timeScale = 1f;

			#if UNITY_EDITOR_64
			print (" Resume!");
			#endif
		}
	}

	public void Settings () {

	}

	public void LevelUp (Transform trans) {

	}

	public void LoseLife () {
		gameSettings.leftLife--;

	}

	public void DestroySiblingCollision () {

	}
}
