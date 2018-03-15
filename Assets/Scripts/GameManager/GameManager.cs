using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameSettings gameSettings;
	public int currentLevelNumber = 0;
	public Transform levels;
	public TextController textController;
	public Transform player;

	public float waitTime;
	private LifeController lifeController;

	private List<Transform> levelsList = new List<Transform> ();
	private int leftLife;
	private Transform currentLevel;

	void Awake () {
		leftLife = gameSettings.leftLife;
	}

	void Start () {
		foreach (Transform trans in levels) {
			levelsList.Add (trans);
		}


		currentLevel = levelsList [currentLevelNumber];

		//find the life Controller
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

		// 需要处理如下事务：
		// 1.关闭当前层级的 长条Collider。 2.disable 子物体。

		// get the collision of current level, enable Trigger and disable Collision
		Transform childTrans = currentLevel.GetChild (0);
		currentLevel.GetComponent <BoxCollider2D> ().enabled = false;

		childTrans.gameObject.SetActive (false);

	}

	IEnumerator Setup (GameObject go) {
		yield return new WaitForSeconds (waitTime);
		go.GetComponent<PolygonCollider2D> ().enabled = true;
		go.GetComponent<BoxCollider2D> ().enabled = false;


		go.GetComponentInParent<BoxCollider2D> ().enabled = true;

	}

	// 在发送之后我们做了什么：
	// 首先 是 LaunchSetting 做的事要处理。
	// 其次是 这个球如果碰到了长条碰撞器，怎么处理。如果没有碰到，怎么处理。

	public void Reset () {

		// reset the next level collider
		Transform nextLevel = levelsList [currentLevelNumber + 1];
		nextLevel.GetComponent<BowlReset> ().Reset ();


		Rigidbody2D rb2d = player.GetComponent <Rigidbody2D> ();

		Transform childTrans = currentLevel.GetChild (0);
		childTrans.gameObject.SetActive (true);
		currentLevel.GetComponent <BoxCollider2D> ().enabled = true;

		rb2d.isKinematic = true;
		player.position = currentLevel.GetChild (1).transform.position;


		BowlMovement bowlMove = currentLevel.GetComponent<BowlMovement> ();

		rb2d.isKinematic = false;
	}

	public void SetUpBeforeFall (GameObject nextLevel) {
		
		StartCoroutine ("Setup", nextLevel);
	}

	public bool LandingOver (Transform trans, bool downDirection) {


		if (trans != currentLevel ) { //&& downDirection
			currentLevelNumber++;
			textController.OnChangeLevel (currentLevelNumber);
			currentLevel = levelsList [currentLevelNumber];

			// 打开下一层级 Jump 触发器开关
			currentLevel.GetComponent<BoxCollider2D> ().enabled = true;

			return true;
		} else {
			return false;
		}

	}

	public void LevelUp () {

	}

}
