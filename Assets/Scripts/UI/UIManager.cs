using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[Tooltip ("index 0 enble, 1 disable")]
	public Sprite[] volumeSprites;
	public Text lifeText;
	public LevelText levelText;
	public GameObject failPanel;
	public GameObject settingsPanel;
	public Image volumeImg;
	public Slider volumeSilder;

	private bool isFailPanelDisplay = false;
	private bool isSettingsPanelDisplay = false;
	private LifeController lifeController;

	private GameManager gm;
	private PlayerSettings playerSettings;

	private float lastVolume = .5f;
	private float currentVolume = .5f;

	void Start () {
		lifeController = FindObjectOfType <LifeController> ();
		gm = FindObjectOfType <GameManager> ();
		playerSettings = FindObjectOfType <PlayerSettings> ();
	}


	private void SettingsPanelDisplay () {
		if (!isSettingsPanelDisplay) {
			settingsPanel.SetActive (true);
			lastVolume = playerSettings.GetSoundVolume ();
			gm.DisableInput ();
		} else {
			settingsPanel.SetActive (false);
			gm.EnableInput ();
			playerSettings.SetSoundVolume (currentVolume);

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
		FailPanelDisplay ();
	}

	public void Restart () {
		FailPanelDisplay ();
		lifeController.Restart ();
		levelText.OnChangeLevel (0);
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


	#endregion

}
