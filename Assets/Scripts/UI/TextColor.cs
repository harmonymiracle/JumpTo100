using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class TextColor : MonoBehaviour {

	public Text text;
	public Color color;

	private float xCoord;
	private float yCoord;

	private float range = 10f;
	private float sample;

	void Start () {
		text = GetComponent <Text> ();
		StartCoroutine (CalcNoise ());
	}


	IEnumerator CalcNoise () {
		while (true) {
			color = new Color (PerlinSample (), PerlinSample (), PerlinSample ());
			text.color = color;
			yield return new WaitForSeconds (.3f);
		}

	}

	public float PerlinSample () {
		xCoord = Random.Range (-range, range);
		yCoord = Random.Range (-range, range);
		return Mathf.PerlinNoise (xCoord, yCoord);
	}


}
