using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlTriggerController : MonoBehaviour {


	private BoxCollider2D col;
	private BoxCollider2D parentCol;

	void Awake () {
		col = GetComponent <BoxCollider2D> ();
		parentCol = transform.parent.GetComponent <BoxCollider2D> ();

	}

	void OnTriggerEnter2D (Collider2D collider) {
		GameObject go = collider.gameObject;

		if (go.CompareTag (TagsManager.PLAYER)) {

			col.enabled = false;
			parentCol.enabled = true;
		}
	}

	public void NextLevelReset () {
		col.enabled = true;
		parentCol.enabled = false;
	}

	public void CurrentLevelReset () {
		col.enabled = false;
		parentCol.enabled = true;
	}

}

