using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Pause : MonoBehaviour {

	[Tooltip ("0 not pause, 1 pause")]
	public Sprite[] pauseSprites;
	public bool isPause = false;

	private Image pausImg;
	private GameManager gm;

	void Awake () {
		pausImg = GetComponent<Image> ();
	}
//
//	void Start () {
//		gm = FindObjectOfType<GameManager> ();
//	}
//
	public void PauseGame () {
		if (isPause) {
			pausImg.sprite = pauseSprites [0];
			isPause = false;
			Time.timeScale = 1f;
		} else {
			pausImg.sprite = pauseSprites [1];
			isPause = true;
			Time.timeScale = 0f;
		}
	}

}
