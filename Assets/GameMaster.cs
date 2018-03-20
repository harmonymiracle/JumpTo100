using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameMaster : MonoBehaviour {

	private PlayerSettings playerSettings;
	private StartMenu startMenu;

	private bool hasSave;
	public bool HasSave {
		get {
			hasSave = playerSettings.IsExistSave ();
			return hasSave;
		}
	}


	public bool isNewGame;

	public void Awake () {
		PlayerPrefs.DeleteAll ();

		DontDestroyOnLoad (gameObject);
	}

	public void Start () {
		playerSettings = GetComponent <PlayerSettings> ();
		LoadNextScene ();
	}


	public void LoadNextScene () {
		SceneManager.LoadScene (StringsManager.Scene_Start);
	}


}
