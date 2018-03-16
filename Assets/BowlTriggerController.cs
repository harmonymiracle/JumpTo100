using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlTriggerController : MonoBehaviour {

	
	private BoxCollider2D col;
	private BoxCollider2D parentCol;

	void Awake () {
		col = GetComponent <BoxCollider2D> ();
		parentCol = GetComponentInParent <BoxCollider2D> ();

	}

	void OnTriggerEnter2D (Collider2D col) {
		GameObject go = col.gameObject;
		if (go.CompareTag (TagsManager.PLAYER)) {
			col.enabled = false;
			parentCol.enabled = true;
		}
	}

	public void Reset () {
		col.enabled = true;
		parentCol.enabled = false;
	}

}
