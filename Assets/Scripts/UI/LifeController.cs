using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {

	public Text lifeText;

	[Tooltip ("Index 0 for life, index 1 for death")]
	public Sprite[] sprites;

	[Tooltip ("The three icons left top")]
	public Image[] lifeImages;
	private int lifeAmount;

	void Start () {
		lifeAmount = lifeImages.Length;
	}


	public void ReplaceLifeIcon () {
		if (lifeAmount >= 1 && lifeAmount <= 3) {
			lifeImages [lifeAmount - 1].sprite = sprites [1];
			lifeAmount--;
		}
		UpdateText (lifeAmount);
	}

	public void RecoverLifeIcon () {
		if (lifeAmount <= 3) {
			lifeImages [lifeAmount - 1].sprite = sprites [0];
			lifeAmount++;
		}
		UpdateText (lifeAmount);
	}

	public void Restart () {
		lifeAmount = lifeImages.Length;
		StartCoroutine (LifeIncreaseEffect ());

	}

	void UpdateText (int amount) {
		lifeText.text = amount.ToString ();
	}

	// 这个函数主要用于让 生命恢复效果显得不要太突兀，总之：特效
	IEnumerator LifeIncreaseEffect () {
		for (int i = 0; i < lifeAmount; i++) {
			yield return new WaitForSeconds (.3f);
			lifeImages[i].sprite = sprites [0];
			UpdateText ((i + 1));
		}
	}


}
