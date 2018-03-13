using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	
	void Start () {
		if (gameObject.tag.Equals ("Untagged")) {
			print ("MyTag:" +gameObject.tag);

		}
	}
	
	

}
