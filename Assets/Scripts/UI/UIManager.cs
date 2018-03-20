using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	[Tooltip ("index 0 enble, 1 disable")]
	public Sprite[] volumeSprites;
	public Text lifeText;
	public LevelText levelText;
	public GameObject failPanel;
	public GameObject settingsPanel;
	public Image volumeImg;
	public Slider volumeSilder;
	public Text failPanelText;

	private bool isFailPanelDisplay = false;
	private bool isSettingsPanelDisplay = false;
	private LifeController lifeController;

	private GameManager gm;
	private GameSettings gameSettings;
	private float lastVolume = .5f;
	private float currentVolume = .5f;

	void Awake () {
		// 该语句 需要在GameManager Start 之前，因为有时需要刷新LifeText
		lifeController = FindObjectOfType <LifeController> ();
	}

	void Start () {
		
		gm = FindObjectOfType <GameManager> ();
		gameSettings = gm.gameSettings;
		currentVolume = gameSettings.soundVolume;
	}


	private void SettingsPanelDisplay () {
		if (!isSettingsPanelDisplay) {
			settingsPanel.SetActive (true);
			gm.DisableInput ();
			currentVolume = gameSettings.soundVolume;
		} else {
			settingsPanel.SetActive (false);
			gm.EnableInput ();
			gameSettings.soundVolume = currentVolume;
		}
		isSettingsPanelDisplay = !isSettingsPanelDisplay;
	}

	private void FailPanelDisplay () {
		if (!isFailPanelDisplay) {
			failPanel.SetActive (true);
			gm.DisableInput ();

		} else {
			failPanel.SetActive (false);
			gm.EnableInput ();
		}

		isFailPanelDisplay = !isFailPanelDisplay;

	}

	public void OnChangeLevel (int level) {
		levelText.OnChangeLevel (level);
	}

	public void LoseLife () {
		lifeController.ReplaceLifeIcon ();
	}

	public void LifeReward () {
		lifeController.RecoverLifeIcon ();
	}

	public void Fail () {
		failPanelText.text = gm.currentLevelNumber.ToString();
		FailPanelDisplay ();

	}

	public void Restart () {
		if (isFailPanelDisplay) {
			FailPanelDisplay ();
		}
		if (isSettingsPanelDisplay) {
			SettingsPanelDisplay ();
		}

		lifeController.Restart ();
		levelText.OnChangeLevel (0);
		gm.Restart ();
	}

	public void RefreshLifeText () {
		lifeController.RefreshLifeText ();
	}

	public void MuteMusic () {
		volumeSilder.value = 0;
	}

	public void OnChangeVolumeValue () {
		
		currentVolume = volumeSilder.value;

		if (currentVolume == 0f) {
			volumeImg.sprite = volumeSprites [1];
		} else if (lastVolume == 0f) {
			volumeImg.sprite = volumeSprites [0];
		}

		lastVolume = currentVolume;
	}

	#region Button' Action
	public void ToggleSettingsPanel () {
		
		SettingsPanelDisplay ();
	}

	public void ReturnToMainMenu () {
		gameSettings.soundVolume = currentVolume;
		gm.ReturnToMainMenu ();
	}


	#endregion

}
