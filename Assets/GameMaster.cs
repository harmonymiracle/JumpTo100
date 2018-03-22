using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameMaster : MonoBehaviour {

	private PlayerSettings playerSettings;
	private SceneLoadManager sceneLoadManager;
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
		DontDestroyOnLoad (gameObject);
		sceneLoadManager = GetComponent <SceneLoadManager> ();
	}

	public void Start () {
		playerSettings = GetComponent <PlayerSettings> ();
		sceneLoadManager.LoadStartScene ();
	}

}
