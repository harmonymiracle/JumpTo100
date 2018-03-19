using UnityEngine;

public class AudioManager : MonoBehaviour {


	public AudioSource playerAudioSource;
	public AudioListener audioListener;

	private AudioClip jumpAudioClip;

	private GameManager gm;

	void Start () {
		jumpAudioClip = playerAudioSource.clip;

	}

	public void JumpSound () {
		playerAudioSource.Play ();
	}


}
