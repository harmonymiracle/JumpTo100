using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Pause : MonoBehaviour {

	public Sprite[] pauseSprites;

	private Image pausImg;

	public bool isPause = false;
	
	private GameManager gm;

	void Awake () {
		pausImg = GetComponent<Image> ();
	}

	void Start () {
		gm = FindObjectOfType<GameManager> ();
	}

	public void PauseGame () {
		if (isPause) {
			pausImg.sprite = pauseSprites [0];
			isPause = false;
		} else {
			pausImg.sprite = pauseSprites [1];
			isPause = true;
		}
		gm.PauseGame (isPause);
	}
}
