using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

	private PlayerSettings playerSettings;

	public void Awake () {

		DontDestroyOnLoad (gameObject);
	}

	public void Start () {
		playerSettings = GetComponent <PlayerSettings> ();
		LoadNextScene ();
	}


	public void LoadNextScene () {
		SceneManager.LoadScene (StringsManager.Start);
	}

	public void Save () {

	}

	public void Load () {

	}



}
