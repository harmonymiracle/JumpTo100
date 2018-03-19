using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public const int TOTALLEVELNUMBER = 10;
	public const float LEVELOFFSETHEIGHT = 2.2f;

	public GameSettings gameSettings;

	private bool isInputActive = true;

	public bool IsInputActive {
		get { return isInputActive; }
	}

	public int currentLevelNumber = 0;

	public UIManager myUIManager;

	public Transform player;
	public float ballSpawnOffset = .25f;

	public Transform levels;
	public GameObject levelPrefab;
	private List<Transform> levelsList = new List<Transform> ();
	private int leftLife;
	private Transform currentLevel;
	private Transform nextLevel;

	// 为性能优化，将一部分变量作为全局变量
	// 该变量是生成 level 所需要的
	private Vector3 levelPosition;

	public CameraFollow2D cameraFollow;

	#region audio
	private AudioManager audioManager;

	#endregion


	void Awake () {
		audioManager = GetComponent<AudioManager> ();
		Init ();
	}


	void Init () {

		// 初始化相关游戏设置 属性
		leftLife = gameSettings.startLife;

		// 如果是第一次进入场景
		if (levelsList.Count <= 2) {
			// 随机生成不同位置，不同速度的level
			float currentHeight = 2.2f + LEVELOFFSETHEIGHT;
			for (int i = 0; i < TOTALLEVELNUMBER - 1; i++) {

				// 加上 .1f 的偏移是防止 bowl坐标超限，造成某些麻烦。
				levelPosition = Vector3.right * Random.Range (-gameSettings.screenHorizontalLimit + .1f, gameSettings.screenHorizontalLimit)
				+ Vector3.up * currentHeight;

				GameObject go = Instantiate (levelPrefab, levelPosition, Quaternion.identity, levels);
				go.GetComponent<BowlMovement> ().moveSpeed = GetSpeedByDifficulty(gameSettings.gameDifficulty);

				currentHeight += LEVELOFFSETHEIGHT;

			}

		} else {   // 如果是重玩模式
			// 初始化初始小碗
			levelsList [0].GetComponent<BoxCollider2D> ().enabled = true;

			for (int i = 1; i < levelsList.Count; i++) {
				levelsList [i].GetComponentInChildren<BoxCollider2D> ().enabled = true;
			}

			currentLevelNumber = 0;
			currentLevel = levelsList [currentLevelNumber];
			ResetPlayer ();

		}
	}

	void Start () {
		foreach (Transform trans in levels) {
			levelsList.Add (trans);
		}

		currentLevel = levelsList [currentLevelNumber];

		//find the life Controller
		myUIManager = FindObjectOfType <UIManager> ();

	}

	public float GetSpeedByDifficulty (GameDifficulty difficulty) {
		// 个人设置：初级1~2.5，中级 1.5~2.5，高级2~3，顶级2.5~3.5
		switch (difficulty) {
		case GameDifficulty.Easy:
			return Random.Range (1f, 2.5f);
			print ("This is the easy mode");
			break;
		case GameDifficulty.Normal:
			return Random.Range (1.5f, 2.5f);
			break;
		case GameDifficulty.Difficulty:
			return Random.Range (2f, 3f);
			break;
		case GameDifficulty.Hard:
			return Random.Range (2.5f, 3.5f);
			break;
		default:
			return Random.Range (1.5f, 2.5f);
		}

	}

	public void DisableInput () {
		isInputActive = false;
	}

	public void EnableInput () {
		isInputActive = true;
	}

	public void Restart () {

		// 关闭 失败 面板
		myUIManager.Restart ();
		Init ();
		cameraFollow.GetComponent <CameraFollow2D> ().ResetPosition ();

	}

	public void ReturnToMainMenu () {
		
		Save ();
		SceneManager.LoadScene (StringsManager.Start);
	}

	public void Save () {

	}

	private void Fail () {
		myUIManager.Fail ();
		isInputActive = false;
	}


	#region Jump And Fall Manage Area

	public void JumpSound () {
		audioManager.JumpSound ();
	}

	public void LoseLife () {
		
		// 此处的执行顺序很重要，如果不清楚，可能会 Index out of range
		if (leftLife >= 1) {
			myUIManager.LoseLife ();
			Reset ();
		} else {
			Fail ();
		}

		leftLife--;
	}


	void Reset () {

		//重置碗的 触发器状态
		nextLevel = levelsList [currentLevelNumber + 1];
		nextLevel.GetChild(0).GetComponent<BowlTriggerController> ().NextLevelReset();
		currentLevel.GetChild(0).GetComponent<BowlTriggerController> ().CurrentLevelReset();

		ResetPlayer ();

	}


	// 重置角色的速度、位置状态
	public void ResetPlayer () {
		player.SetParent (currentLevel);
		player.GetComponent<PlayerMove> ().Reset ();
		player.localPosition = Vector3.zero + Vector3.up * ballSpawnOffset;
	}

	public void LaunchSetting () {
		JumpSound ();
		currentLevel.GetComponent <BoxCollider2D> ().enabled = false;
	}


	public void LevelUp (Transform trans) {
		if (trans != currentLevel) { 

			// 禁用之前层级的触发器
			currentLevel.GetComponent <BoxCollider2D> ().enabled = false;
			currentLevelNumber++;
			currentLevel = levelsList [currentLevelNumber];
			myUIManager.OnChangeLevel (currentLevelNumber);

			ResetPlayer ();
		} 

	}

	#endregion

}
