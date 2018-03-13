using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class TextController : MonoBehaviour {

	private Text text;

	void Awake () {
		text = GetComponent <Text> ();
	}
	void Start () {
		
	}
	
	
	void Update () {
		
	}

	void OnChangeLevel (int level) {

		text.text = level.ToString ();

		print ("level: " + level);
	}
}
