using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 该类目的是 存储状态信息到 本地，也就是PlayerPrefs。
// 不过在有 ScriptableObject Asset的情况下，好像可以省却这一步，那个本身就可以作为存档。
// 并且其优越性可能更高，因为Asset可以存储对象。

public class PlayerSettings : MonoBehaviour {

	// 一些字符串，用于PlayerPrefs 存储的key
	public const string SOUND_VOLUME = StringsManager.SOUND_VOLUME;
	public const string HIGHEST_LEVEL = StringsManager.HIGHEST_LEVEL;
	public const string CURRENT_DIFFICULTY = StringsManager.CURRENT_DIFFICULTY;
	public const string CURRENT_LEVEL = StringsManager.CURRENT_LEVEL;
	public const string LEFT_LIFE = StringsManager.LEFT_LIFE;

	private float soundVolume = .5f;
	private int highestLevelHistory = 0;
	private int currentLevel = 0;
	private GameDifficulty currentDifficulty;
	private int leftLife = 3;
	private bool existSave = false;

	public float SoundVolume {
		get {
			soundVolume = PlayerPrefs.GetFloat (SOUND_VOLUME);
			return soundVolume;
		}
		set {
			soundVolume = value;
			PlayerPrefs.SetFloat (SOUND_VOLUME, soundVolume);
		}
	}

	public int HighestLevelHistory {
		get {
			highestLevelHistory = PlayerPrefs.GetInt (HIGHEST_LEVEL);
			return highestLevelHistory;
		}
		set {
			highestLevelHistory = value;
			PlayerPrefs.SetInt (HIGHEST_LEVEL, highestLevelHistory);
		}
	}

	public int CurrentLevel {
		get {
			currentLevel = PlayerPrefs.GetInt (CURRENT_LEVEL);
			return currentLevel;
		}
		set {
			currentLevel = value;
			PlayerPrefs.SetInt (CURRENT_LEVEL, currentLevel); 
		}
	}

	public GameDifficulty CurrentDifficulty {
		get {
			string difficulty = PlayerPrefs.GetString (CURRENT_DIFFICULTY);
			if (difficulty != "") {
				switch (difficulty) {
				case "Easy":
					currentDifficulty = GameDifficulty.Easy;
					break;
				case "Normal":
					currentDifficulty = GameDifficulty.Normal;
					break;
				case "Difficulty":
					currentDifficulty = GameDifficulty.Difficulty;
					break;
				case "Hard":
					currentDifficulty = GameDifficulty.Hard;
					break;
				}
			} 
			return currentDifficulty;
		}
		set {
			currentDifficulty = value;
			PlayerPrefs.SetString (CURRENT_DIFFICULTY, currentDifficulty.ToString()); 
		}
	}

	public int LeftLife {
		get {
			// 这里可能会有问题，如果没有存档的话，先测试一下
			if (PlayerPrefs.HasKey (LEFT_LIFE)) {
				leftLife = PlayerPrefs.GetInt (LEFT_LIFE);

				return leftLife;
			} 
			return 3;
		}
		set {
			if (leftLife >= 0) {
				leftLife = value;
			} else {
				leftLife = 3;
			}
			PlayerPrefs.SetInt (LEFT_LIFE, leftLife);
		}
	}

	public bool IsExistSave () {

		return PlayerPrefs.GetInt(CURRENT_LEVEL) != 0;
	}
		
}
