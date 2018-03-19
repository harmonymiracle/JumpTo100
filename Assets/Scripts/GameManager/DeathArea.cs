using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour {

	private BoxCollider2D boxCollider;
	private GameManager gm;
	void Awake () {
		boxCollider = GetComponent<BoxCollider2D> ();
		gm = FindObjectOfType<GameManager> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		switch (col.gameObject.tag) {
		case TagsManager.PLAYER:

			gm.LoseLife ();
			break;
		}
	}
}
