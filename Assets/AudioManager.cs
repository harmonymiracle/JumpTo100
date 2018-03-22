using UnityEngine;

public class AudioManager : MonoBehaviour {

	public SceneLoadManager sceneLoadManager;

	public AudioClip startBgm;

	public AudioClip jumpSound;
	public AudioClip landSound;

	private AudioSource audioSource;

	private AudioSource playerAudioSource;


//	public AudioSource playerAudioSource;
//
//	private AudioClip jumpAudioClip;
//

	void Awake () {
		DontDestroyOnLoad (gameObject);
		audioSource = GetComponent <AudioSource> ();

	}

	void OnEnable () {
		sceneLoadManager.OnLoadStartScene += LoadStartScene;
		sceneLoadManager.OnLoadGameScene += LoadGameScene;
	}

	void OnDisable () {
		sceneLoadManager.OnLoadStartScene -= LoadStartScene;
		sceneLoadManager.OnLoadGameScene -= LoadGameScene;

	}


	public void LandSound () {
		playerAudioSource.clip = landSound;
		playerAudioSource.Play ();
	}

	public void JumpSound () {
		playerAudioSource.clip = jumpSound;
		playerAudioSource.Play ();
	}


	void LoadStartScene () {
		
		audioSource.clip = startBgm;
		audioSource.volume = .8f;
		audioSource.loop = true;
		audioSource.Play ();

	}

	void LoadGameScene () {
		playerAudioSource = GameObject.FindGameObjectWithTag (TagsManager.PLAYER).GetComponent <AudioSource> ();

	}

}
