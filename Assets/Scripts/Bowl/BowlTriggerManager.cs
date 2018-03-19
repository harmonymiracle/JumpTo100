using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlTriggerManager : MonoBehaviour {


	public float maxDistanceToGrounded = .5f;
	public float maxHeightToGrounded = .15f;

	// 在层级中， index = 0是高度触发层级 => 触发后者，index = 1 为 落点触发器，代表升级以及未起跳
	private BoxCollider2D boxCollider;

	private GameManager gm;


	void Awake () {
		boxCollider = GetComponent <BoxCollider2D> ();
		gm = FindObjectOfType <GameManager> ();
	}

	void Reset () {
		boxCollider.enabled = false;
	}

	void OnTriggerEnter2D (Collider2D col) {
		GameObject go = col.gameObject;

		if (go.CompareTag (TagsManager.PLAYER)) {
			if (Mathf.Abs (go.transform.position.x - transform.position.x) <= maxDistanceToGrounded &&
				go.transform.position.y > transform.position.y + maxHeightToGrounded) {
				gm.LevelUp (transform);
			}
		}
	}


}
