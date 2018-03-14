using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Text))]
public class TextController : MonoBehaviour {

	private Text text;
	private Animator anim;

	private bool isFirst = true;

	void Awake () {
		text = GetComponent <Text> ();
		anim = GetComponent <Animator> ();
	}

	public void OnChangeLevel (int level) {
		
		AnimControl (level);
	}

	void AnimControl (int level) {
		
		StartCoroutine ("DelayFixedTime", level); 
	}

	IEnumerator DelayFixedTime (int level) {
		anim.SetBool ("Level Change", true);
		yield return null;
//		AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo (0);
//		AnimationClip clip = clipInfo [0].clip;
//
//		if (isFirst) {
//			AnimationEvent evt = new AnimationEvent ();
//			evt.intParameter = level;
//			evt.time = .5f;
//			evt.functionName = "ChangeText";
//
//			clip.AddEvent (evt);
//			ChangeText (level);
//			isFirst = false;
//		} else {
//			clip.events [0].intParameter = level;
//		}

		yield return new WaitForSeconds (1f); //clipInfo [0].clip.length
		anim.SetBool ("Level Change", false);
		ChangeText (level);
	}

	public void ChangeText (int level) {
		text.text = level.ToString ();
	}

}
