using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour {

	public const int TOTALLEVELNUMBER = 30;
	public const float LEVELOFFSETHEIGHT = 2.2f;
	public float ballSpawnOffset = .25f;

	public GameSettings gameSettings;


	public GameObject levelPrefab;
	private List<Transform> levelsList = new List<Transform> ();

	public int currentLevelNumber = 0;

	private Transform currentLevel;
	private Transform nextLevel;

	// 为性能优化，将一部分变量作为全局变量
	// 该变量是生成 level 所需要的
	private Vector3 levelPosition;
	private GameManager gm;

	private Transform player;
	private Transform levels;

	void Awake () {
		levels = transform;

		// 这里最好在这里寻找Player，不然之后如果继续游戏，很可能 Reset的时候空引用
		player = GameObject.FindGameObjectWithTag (TagsManager.PLAYER).transform;
		if (player == null) {
			print ("null");
		}
	}

	void Start () {
		gm = FindObjectOfType <GameManager> ();

	}

	public void PopulateLevels () {

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

		StartCoroutine (DelayUpdateFirstTime ());

	}
		

	public void ContinueGame (int currentLevel) {
		
		// 此处的顺序也很重要，如果更新数据在前，而此时还未生成，就会索引超限
		PopulateLevels ();

		currentLevelNumber = currentLevel;
		StartCoroutine (DelayUpdateFirstTime ());

		levelsList [0].GetComponent<BoxCollider2D> ().enabled = false;
		for (int i = 1; i <= currentLevelNumber; i++) {
			levelsList [i].GetComponentInChildren <BoxCollider2D> ().enabled = false;
		}
		levelsList [currentLevelNumber].GetComponent <BoxCollider2D> ().enabled = true;
		ResetPlayer ();
	}

	public void ReplayLevelsSetup () {
		// 如果是重玩模式
		// 初始化初始小碗
		levelsList [0].GetComponent<BoxCollider2D> ().enabled = true;

		for (int i = 1; i < levelsList.Count; i++) {
			levelsList [i].GetComponentInChildren<BoxCollider2D> ().enabled = true;
		}

		currentLevelNumber = 0;
		UpdateCurrentInfo ();

		ResetPlayer ();
	}

	public void Reset () {
		nextLevel = levelsList [currentLevelNumber + 1];
		nextLevel.GetComponentInChildren<BowlTriggerController> ().NextLevelReset();
		currentLevel.GetComponentInChildren<BowlTriggerController> ().CurrentLevelReset();

		ResetPlayer ();
	}

	public bool LevelUp (Transform trans) {

		if (trans != currentLevel && trans.GetSiblingIndex () > currentLevelNumber) { 

			// 禁用之前层级的触发器
			currentLevel.GetComponent <BoxCollider2D> ().enabled = false;
			currentLevelNumber++;
			UpdateCurrentInfo ();
//
//			gameSettings.currentLevel++;
//			currentLevel = levelsList [currentLevelNumber];

			ResetPlayer ();
			return true;
		} else {
			return false;
		}

	}

	// 重置角色的速度、位置状态
	public void ResetPlayer () {

		if (player != null) {
			player.SetParent (currentLevel);
			player.GetComponent<PlayerMove> ().Reset ();
			player.localPosition = Vector3.zero + Vector3.up * ballSpawnOffset;
		} else {
			print ("null");
		}


	}

	public void UpdateCurrentInfo () {
		gameSettings.currentLevel = currentLevelNumber;
		currentLevel = levelsList [currentLevelNumber];
		gm.currentLevelNumber = currentLevelNumber;
		gm.currentLevel = currentLevel;
	}

	public IEnumerator DelayUpdateFirstTime () {
		gameSettings.currentLevel = currentLevelNumber;
		currentLevel = levelsList [currentLevelNumber];
		yield return null;
		gm.currentLevelNumber = currentLevelNumber;
		gm.currentLevel = currentLevel;

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
}
