using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour {

	public GameSettings gameSettings;

	public float waitTime = .7f;

	// if the distance < distanceToGrounded, it was considered Grounded
	public float distanceToGrounded = .4f;
    
	private CircleCollider2D mycollider;
	private Rigidbody2D rb2d;

	private bool isGrounded = true;

	private Transform myTransform;
	private GameManager gm;
	void Awake () {
		myTransform = transform;
		mycollider = GetComponent<CircleCollider2D> ();
		rb2d = GetComponent <Rigidbody2D> ();
		gm = FindObjectOfType<GameManager> ();

	}

	void FixedUpdate () {

	}

//	void OnCollisionEnter2D (Collision2D col) {
//		if (col.gameObject.tag.Equals (TagsManager.BOWL)) {
//			isGrounded = true;
//		}
//	}

	void OnTriggerEnter2D (Collider2D col) {
		GameObject go = col.gameObject;

		switch (go.tag) {
		case TagsManager.BOWL:
			isGrounded = true;

			transform.SetParent (go.transform);
			break;
		case TagsManager.COLLISION:
			StartCoroutine ("Setup", go);
			break;
		default:
			break;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		GameObject go = col.gameObject;
		switch (go.tag) {
		case TagsManager.DEATHAREA:
			
			break;
		}
	}

	IEnumerator Setup (GameObject go) {
		yield return new WaitForSeconds (waitTime);
		go.GetComponent<PolygonCollider2D> ().enabled = true;
		go.GetComponent<BoxCollider2D> ().enabled = false;

	}

	public void Move () {

		if (isGrounded) {
			rb2d.AddForce (gameSettings.jumpForce, ForceMode2D.Impulse);
			isGrounded = false;

			myTransform.parent.GetChild (1).GetComponent<PolygonCollider2D> ().enabled = false;
		}
	}
}
