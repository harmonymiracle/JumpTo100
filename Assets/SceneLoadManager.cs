using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SceneLoadManager : MonoBehaviour {

	public Action OnLoadGameScene;
	public Action OnLoadStartScene;

	private int currentSceneIndex = 0;
	public int CurrentSceneIndex {
		get { return currentSceneIndex; }
	}

	private string currentSceneName = StringsManager.Scene_Start;
	public string CurrentSceneName {
		get { return currentSceneName; }
	}

		
	public void LoadNextScene () {
		SceneManager.LoadScene (currentSceneIndex ++);

	}

	public void LoadStartScene () {
		currentSceneName = StringsManager.Scene_Start;
		SceneManager.LoadScene (StringsManager.Scene_Start);
		if (OnLoadStartScene != null) {
			OnLoadStartScene ();
		}
	}

	public void LoadGameScene () {
		currentSceneName = StringsManager.Scene_Level1;
		SceneManager.LoadScene (StringsManager.Scene_Level1);

		if (OnLoadGameScene != null) {
			OnLoadGameScene ();
		}
	}




}
