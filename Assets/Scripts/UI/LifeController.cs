using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {

	[Tooltip ("Index 0 for life, index 1 for death")]
	public Sprite[] sprites;
	public Image[] lifeImages;
	private int lifeAmount;

	void Start () {
		lifeAmount = lifeImages.Length;
	}


	public void ReplaceLifeIcon () {
		if (lifeAmount >= 1) {
			lifeImages [lifeAmount - 1].sprite = sprites [1];
			lifeAmount--;
		}
	}


}
