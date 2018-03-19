using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour {

	// 一些字符串，用于PlayerPrefs 存储的key
	public const string SOUND_VOLUME = StringsManager.SOUND_VOLUME;
	public const string HIGHEST_LEVEL = StringsManager.HIGHEST_LEVEL;
	public const string CURRENT_DIFFICULTY = StringsManager.CURRENT_DIFFICULTY;
	public const string CURRENT_LEVEL = StringsManager.CURRENT_LEVEL;

	private float preferSoundVolume = .5f;
	private int highestLevelHistory = 0;

	private int currentLevel = 0;
	private GameDifficulty currentDifficulty;



	public float PreferSoundVolume {
		get { return preferSoundVolume; }
		set { preferSoundVolume = value; }
	}

	public int HighestLevelHistory {
		get { return highestLevelHistory; }
		set { highestLevelHistory = value; }
	}

	public int CurrentLevel {
		get { return currentLevel; }
		set { currentLevel = value; }
	}

	public GameDifficulty CurrentDifficulty {
		get { return currentDifficulty; }
		set { currentDifficulty = value; }
	}

		
	public void SetSoundVolume (float volume) {
		preferSoundVolume = volume;
		PlayerPrefs.SetFloat (SOUND_VOLUME, preferSoundVolume);
	}

	public void SetHighestLevelHistory (int level) {
		highestLevelHistory = level;
		PlayerPrefs.SetInt (HIGHEST_LEVEL, highestLevelHistory);
	}

	public void SetCurrentLevel (int level) {
		currentLevel = level;
		PlayerPrefs.SetInt (CURRENT_LEVEL, currentLevel);
	}

	public void SetGameDifficulty (GameDifficulty difficulty) {
		currentDifficulty = difficulty;
		PlayerPrefs.SetString (CURRENT_DIFFICULTY, currentDifficulty.ToString());
	}

	public float GetSoundVolume () {
		preferSoundVolume = PlayerPrefs.GetFloat (SOUND_VOLUME);
		return preferSoundVolume;
	}

	public int GetHighestLevelHistory () {
		highestLevelHistory = PlayerPrefs.GetInt (HIGHEST_LEVEL);
		return highestLevelHistory;
	}

	public int GetCurrentLevel () {
		currentLevel = PlayerPrefs.GetInt (CURRENT_LEVEL);
		return currentLevel;
	}

	public GameDifficulty GetGameDifficulty () {
		string difficulty = PlayerPrefs.GetString (CURRENT_DIFFICULTY);
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

		return currentDifficulty;
	}


}
