using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class LevelText : MonoBehaviour {

	
	private Text text;
	private Animator anim;

	private bool isFirst = true;

	void Awake () {
		text = GetComponent <Text> ();
		anim = GetComponent <Animator> ();
	}

	public void OnChangeLevel (int level) {

		StartCoroutine ("DelayFixedTime", level); 

	}


	IEnumerator DelayFixedTime (int level) {
		anim.SetBool ("Level Change", true);
		yield return null;
		yield return new WaitForSeconds (1f); //clipInfo [0].clip.length
		anim.SetBool ("Level Change", false);
		ChangeText (level);
	}

	public void ChangeText (int level) {
		text.text = level.ToString ();
	}

}
