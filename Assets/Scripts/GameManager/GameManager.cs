using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public const int TOTALLEVELNUMBER = 30;
	public const float LEVELOFFSETHEIGHT = 2.2f;

	public GameSettings gameSettings;

	// 判断是继续游戏还是 新游戏
	public bool isNewGame = true;

	// 是否处于UI操作中
	private bool isInputActive = true;

	public bool IsInputActive {
		get { return isInputActive; }
	}

	public int currentLevelNumber = 0;

	public UIManager myUIManager;


	public Transform player;
	public float ballSpawnOffset = .25f;
	public CameraFollow2D cameraFollow;

	public Transform levels;
	public GameObject levelPrefab;
	private List<Transform> levelsList = new List<Transform> ();
	private int leftLife;
	private Transform currentLevel;
	private Transform nextLevel;

	// 为性能优化，将一部分变量作为全局变量
	// 该变量是生成 level 所需要的
	private Vector3 levelPosition;

	private PlayerSettings playerSettings;
	private GameMaster gameMaster;
	#region audio
	private AudioManager audioManager;

	#endregion


	void Awake () {
		//audioManager = GetComponent<AudioManager> ();

		audioManager = FindObjectOfType <AudioManager> ();

		gameMaster = FindObjectOfType <GameMaster> ();
		playerSettings = gameMaster.transform.GetComponent <PlayerSettings> ();
		isNewGame = gameMaster.isNewGame;

		//Init ();
	}

	public void NewGame () {
		
		// 如果是第一次进入场景
		if (levelsList.Count <= 2) {
			
			foreach (Transform trans in levels) {
				levelsList.Add (trans);
			}
			// 随机生成不同位置，不同速度的level, 这里之后将2.2f 删除，从而从第一层开始生成
			float currentHeight = 2.2f + LEVELOFFSETHEIGHT;
			for (int i = 0; i < TOTALLEVELNUMBER - 1; i++) {

				// 加上 .1f 的偏移是防止 bowl坐标超限，造成某些麻烦。
				levelPosition = Vector3.right * Random.Range (-gameSettings.screenHorizontalLimit + .1f, gameSettings.screenHorizontalLimit)
					+ Vector3.up * currentHeight;

				GameObject go = Instantiate (levelPrefab, levelPosition, Quaternion.identity, levels);
				go.GetComponent<BowlMovement> ().moveSpeed = GetSpeedByDifficulty(gameSettings.gameDifficulty); 
				levelsList.Add (go.transform);

				currentHeight += LEVELOFFSETHEIGHT;
			}

		} else {   // 如果是重玩模式
			// 初始化初始小碗
			levelsList [0].GetComponent<BoxCollider2D> ().enabled = true;

			for (int i = 1; i < levelsList.Count; i++) {
				levelsList [i].GetComponentInChildren<BoxCollider2D> ().enabled = true;
			}

			currentLevelNumber = 0;
			gameSettings.currentLevel = 0;
			currentLevel = levelsList [currentLevelNumber];
			ResetPlayer ();

		}
		currentLevel = levelsList [currentLevelNumber];
		// 魔数，初始生命恒为三
		if (isNewGame) {
			leftLife = 3;
		} else {
			leftLife = gameSettings.leftLife;
		}

	}


	void Init () {
		if (isNewGame) {
			NewGame ();
			ClearSettings ();
		} else {
			LoadCurrentInfo ();
			NewGame ();
			// 一些读取后的处理, 处理level的触发器状态

			levelsList [0].GetComponent<BoxCollider2D> ().enabled = false;
			for (int i = 1; i <= currentLevelNumber; i++) {
				levelsList [i].GetComponentInChildren <BoxCollider2D> ().enabled = false;
			}
			levelsList [currentLevelNumber].GetComponent <BoxCollider2D> ().enabled = true;
			ResetPlayer ();
			myUIManager.OnChangeLevel (currentLevelNumber);
			myUIManager.RefreshLifeText ();

			cameraFollow.ResetPosition ();

		}
	}

	void Start () {
		Init ();

		//find the life Controller
		myUIManager = FindObjectOfType <UIManager> ();
		playerSettings = FindObjectOfType <PlayerSettings> ();

	}

	public void Restart () {

		// 这两行顺序是有序的，否则 二者生命不照应
		gameSettings.leftLife = 3;
		NewGame ();

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

		// 在这里发声
		audioManager.JumpSound ();

		currentLevel.GetComponent <BoxCollider2D> ().enabled = false;
	}


	public void LevelUp (Transform trans) {
		if (trans != currentLevel && trans.GetSiblingIndex() > currentLevelNumber) { 

			// 禁用之前层级的触发器
			currentLevel.GetComponent <BoxCollider2D> ().enabled = false;
			currentLevelNumber++;
			gameSettings.currentLevel++;
			currentLevel = levelsList [currentLevelNumber];
			myUIManager.OnChangeLevel (currentLevelNumber);

			ResetPlayer ();
		} 

	}

	#endregion


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

}
