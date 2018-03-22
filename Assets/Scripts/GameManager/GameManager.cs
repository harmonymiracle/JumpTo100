using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {

//	public const int TOTALLEVELNUMBER = 30;
//	public const float LEVELOFFSETHEIGHT = 2.2f;
//
	public GameSettings gameSettings;

	// 判断是继续游戏还是 新游戏
	public bool isNewGame = true;

	// 是否处于UI操作中
	private bool isInputActive = true;

	public bool IsInputActive {
		get { return isInputActive; }
	}

	public UIManager myUIManager;


//	public Transform player;
//	public float ballSpawnOffset = .25f;
	public CameraFollow2D cameraFollow;

//	public Transform levels;
//	public GameObject levelPrefab;
//	private List<Transform> levelsList = new List<Transform> ();
	private int leftLife;

	// 保留下最低限度的和Levels 的接口，便于管理其他组件
	[NonSerialized]
	public Transform currentLevel;
	public int currentLevelNumber = 0;
//	private Transform nextLevel;

	private PlayerSettings playerSettings;
	private GameMaster gameMaster;
	#region audio
	private AudioManager audioManager;

	#endregion

	public LevelsManager levelsManager;

	void Awake () {
		audioManager = FindObjectOfType <AudioManager> ();
		levelsManager = FindObjectOfType <LevelsManager> ();
		gameMaster = FindObjectOfType <GameMaster> ();
		playerSettings = gameMaster.transform.GetComponent <PlayerSettings> ();
		isNewGame = gameMaster.isNewGame;
	}


	void Init () {
		if (isNewGame) {
			NewGame ();
			SetLife ();
			ClearSettings ();
		} else {
			LoadCurrentInfo ();
			ContinueGame ();
			SetLife ();

			myUIManager.OnChangeLevel (currentLevelNumber);
			myUIManager.RefreshLifeText ();
			cameraFollow.ResetPosition ();

		}
	}

	void Start () {
		Init ();
		myUIManager = FindObjectOfType <UIManager> ();
		playerSettings = FindObjectOfType <PlayerSettings> ();

	}

	public void NewGame () {
		levelsManager.PopulateLevels ();
	}

	public void ContinueGame () {
		levelsManager.ContinueGame (currentLevelNumber);
	}

	void SetLife () {
		// 魔数，初始生命恒为三
		if (isNewGame) {
			leftLife = 3;
		} else {
			leftLife = gameSettings.leftLife;
		}
	}

	public void Restart () {

		// 这两行顺序是有序的，否则 二者生命不照应
		gameSettings.leftLife = 3;

		levelsManager.ReplayLevelsSetup ();
		SetLife ();
		cameraFollow.ResetPosition ();

	}

	public void ClearSettings () {
		//gameSettings.gameDifficulty = GameDifficulty.Easy;
		gameSettings.leftLife = 3;
		gameSettings.currentLevel = 0;
	}

	public void ReturnToMainMenu () {
		
		SaveCurrentInfo ();
		SceneManager.LoadScene (StringsManager.Scene_Start);
	}

	public void SaveCurrentInfo () {
		playerSettings.SoundVolume = gameSettings.soundVolume;
		playerSettings.CurrentLevel = gameSettings.currentLevel;
		if (currentLevelNumber > playerSettings.HighestLevelHistory) {
			playerSettings.HighestLevelHistory = gameSettings.currentLevel;
		}
		playerSettings.CurrentDifficulty = gameSettings.gameDifficulty;
		playerSettings.LeftLife = gameSettings.leftLife;
	}

	public void LoadCurrentInfo () {
		gameSettings.soundVolume = playerSettings.SoundVolume;
		gameSettings.currentLevel = playerSettings.CurrentLevel;
		gameSettings.gameDifficulty = playerSettings.CurrentDifficulty;
		gameSettings.leftLife = playerSettings.LeftLife;

		// 或许可以用gamesettings 记录整个游戏中的数据
		currentLevelNumber = gameSettings.currentLevel;
	}

	private void Fail () {
		myUIManager.Fail ();
		isInputActive = false;
	}

	#region Jump And Fall Manage Area

	public void LoseLife () {
		
		// 此处的执行顺序很重要，如果不清楚，可能会 Index out of range
		if (leftLife >= 1) {
			myUIManager.LoseLife ();
			Reset ();
		} else {
			Fail ();
		}

		leftLife--;
		gameSettings.leftLife = leftLife;
	}

	void Reset () {
		levelsManager.Reset ();
	}

	public void LaunchSetting () {
		audioManager.JumpSound ();

		currentLevel.GetComponent <BoxCollider2D> ().enabled = false;
	}

	public void LevelUp (Transform trans) {
		if (levelsManager.LevelUp (trans)) {
			myUIManager.OnChangeLevel (currentLevelNumber);
		}

	}

	#endregion

	public void DisableInput () {
		isInputActive = false;
	}

	public void EnableInput () {
		isInputActive = true;
	}

}



// 如果是第一次进入场景
//		if (levelsList.Count <= 2) {
//			
//			foreach (Transform trans in levels) {
//				levelsList.Add (trans);
//			}
//			// 随机生成不同位置，不同速度的level, 这里之后将2.2f 删除，从而从第一层开始生成
//			float currentHeight = 2.2f + LEVELOFFSETHEIGHT;
//			for (int i = 0; i < TOTALLEVELNUMBER - 1; i++) {
//
//				// 加上 .1f 的偏移是防止 bowl坐标超限，造成某些麻烦。
//				levelPosition = Vector3.right * Random.Range (-gameSettings.screenHorizontalLimit + .1f, gameSettings.screenHorizontalLimit)
//					+ Vector3.up * currentHeight;
//
//				GameObject go = Instantiate (levelPrefab, levelPosition, Quaternion.identity, levels);
//				go.GetComponent<BowlMovement> ().moveSpeed = GetSpeedByDifficulty(gameSettings.gameDifficulty); 
//				levelsList.Add (go.transform);
//
//				currentHeight += LEVELOFFSETHEIGHT;
//			}
//
//		} else {   // 如果是重玩模式
//			// 初始化初始小碗
//			levelsList [0].GetComponent<BoxCollider2D> ().enabled = true;
//
//			for (int i = 1; i < levelsList.Count; i++) {
//				levelsList [i].GetComponentInChildren<BoxCollider2D> ().enabled = true;
//			}
//
//			currentLevelNumber = 0;
//			gameSettings.currentLevel = 0;
//			currentLevel = levelsList [currentLevelNumber];
//			ResetPlayer ();
//
//		}
//		currentLevel = levelsList [currentLevelNumber];

