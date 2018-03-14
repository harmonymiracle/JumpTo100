using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameSettings gameSettings;
	public int currentLevel = 0;
	public Transform levels;
	public TextController textController;
	public Transform player;

	public float waitTime;
	private LifeController lifeController;
	private List<Transform> levelsList = new List<Transform> ();
	private int leftLife;
	private Transform lastLevel;

	void Awake () {
		leftLife = gameSettings.leftLife;
	}

	void Start () {
		foreach (Transform trans in levels) {
			levelsList.Add (trans);
		}

		lastLevel = levelsList [0];
		lifeController = FindObjectOfType <LifeController> ();

	}
		

	#region UI Method

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
	#endregion



	public void LoseLife () {
		
		// 此处的执行顺序很重要，如果不清楚，可能会 Index out of range
		if (leftLife >= 1) {
			lifeController.ReplaceLifeIcon ();
			Reset ();
		} else {
			Fail ();
		}

		leftLife--;

	}

	private void Fail () {
		print ("You Failed!");
	}

	public void LaunchSetting () {
		

		// get the collision of current level, enable Trigger and disable Collision
		Transform childTrans = lastLevel.GetChild (0);
		lastLevel.GetComponent <BoxCollider2D> ().enabled = false;

		childTrans.gameObject.SetActive (false);
		print ("childTrans: " + childTrans);

	}

	IEnumerator Setup (GameObject go) {
		yield return new WaitForSeconds (waitTime);
		go.GetComponent<PolygonCollider2D> ().enabled = true;
		go.GetComponent<BoxCollider2D> ().enabled = false;

	}


	public void Reset () {
		Rigidbody2D rb2d = player.GetComponent <Rigidbody2D> ();

		Transform childTrans = lastLevel.GetChild (0);
		childTrans.gameObject.SetActive (true);
		lastLevel.GetComponent <BoxCollider2D> ().enabled = true;

		rb2d.isKinematic = true;
		player.position = lastLevel.GetChild (1).transform.position;


		BowlMovement bowlMove = lastLevel.GetComponent<BowlMovement> ();

		rb2d.isKinematic = false;
		print ("Reset");
	}

	public void SetUpBeforeFall (GameObject nextLevel) {
		
		StartCoroutine ("Setup", nextLevel);
	}

	public bool LandingOver (Transform trans, bool downDirection) {


		if (trans != lastLevel && downDirection) {
			currentLevel++;
			textController.OnChangeLevel (currentLevel);
			lastLevel = levelsList [currentLevel];

			print ("has Jumped, level up");

			return true;
		} else {
			return false;
		}

	}

	public void LevelUp () {

	}

}
