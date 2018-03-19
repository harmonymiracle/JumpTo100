﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour {

	public Text newGameText;
	public RectTransform difficultyPanel;
	public GameObject continueButton;
	public GameSettings gameSettings;

	private GameDifficulty difficulty = GameDifficulty.Easy;


	private bool isShowDifficultyPanel = false;
	private bool isShowAchievementsPanel = false;


	public void ContinueGame () {
		if (PlayerPrefs.HasKey (StringsManager.CURRENT_DIFFICULTY)) {
			continueButton.SetActive (true);
		}
	}

	public void NewGame () {
		SceneManager.LoadScene (StringsManager.Scene_Level1);
	}



	// 这里使用了 “魔数”，是根据是实际情况调整的，后续可以改善
	public void ToggleDifficultyPanel () {
		print ("trans pos: " + difficultyPanel.transform.position);

		if (!isShowDifficultyPanel) {
			difficultyPanel.transform.localPosition = new Vector3 (0, 6f, 0f);
			print ("trans pos: " + difficultyPanel.transform.position);
		} else {
			difficultyPanel.transform.localPosition = new Vector3 (800f, 6f, 0f);
			print ("trans pos: " + difficultyPanel.transform.position);
		}
		isShowDifficultyPanel = !isShowDifficultyPanel;
	}


	public void ToggleAchievements () {

	}

	public void QuitGame () {
		Application.Quit ();
	}


	public void SetDifficulty (int diff) {

		switch (diff) {
		case 0:
			difficulty = GameDifficulty.Easy;
			newGameText.text = StringsManager.NEW_GAME_TEXT + StringsManager.DIFFICULTY_EASY;
			break;
		case 1:
			difficulty = GameDifficulty.Normal;
			newGameText.text = StringsManager.NEW_GAME_TEXT + StringsManager.DIFFICULTY_NORMAL;

			break;
		case 2:
			difficulty = GameDifficulty.Difficulty;
			newGameText.text = StringsManager.NEW_GAME_TEXT + StringsManager.DIFFICULTY_DIFFICULTY;

			break;
		case 3:
			difficulty = GameDifficulty.Hard;
			newGameText.text = StringsManager.NEW_GAME_TEXT + StringsManager.DIFFICULTY_HARD;

			break;
		}

		gameSettings.gameDifficulty = difficulty;

	}

}