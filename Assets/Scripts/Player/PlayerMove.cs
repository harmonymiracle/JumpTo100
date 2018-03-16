using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMove : MonoBehaviour {

	public GameSettings gameSettings;
	public float maxGroundedDistance = .4f;
	public float minWaitForGrounded = .3f;
	public float gravityScale = 1.0f;
	// 当对象在空中时，该对象作为小球的父对象。
	public Transform empty;

//	private bool isGrounded = true;
	private Transform myTransform;
	private GameManager gm;

	private float yVelocity = 0;
	private bool isGrounded = true;

	void Awake () {
		myTransform = transform;
		gm = FindObjectOfType <GameManager> ();
	}
		
//	void FixedUpdate () {
//		CheckIfGrounded ();
//		print (isGrounded);
//	}
//
//	void CheckIfGrounded () {
//
//		Collider2D collider = Physics2D.OverlapCircle (Vector2.zero, maxGroundedDistance);
//
//		if (collider != null) {
//			if (collider.tag.Equals (TagsManager.COLLISION)) {
//				isGrounded = true;
//				print ("right: " + collider.name);
//
//			} else {
//				print ("false: " + collider.name);
//			}
//
//		} else {
//
//			print ("null");
//		}
//
//	}



	void FixedUpdate () {

		if (!isGrounded) {
			ApplyGravity ();

			transform.Translate (Vector2.up * yVelocity * Time.deltaTime);
		} else {
			yVelocity = 0;
		}


	}

	void ApplyGravity () {
		yVelocity += Time.fixedDeltaTime * Physics2D.gravity.y * gravityScale;
	}


	void OnTriggerEnter2D (Collider2D col) {
		GameObject go = col.gameObject;

		if (go.CompareTag (TagsManager.BOWL)) {
			StartCoroutine ("EnableGroundedTrigger");
		}
	}

	void OnTriggerStay2D (Collider2D col) {
		GameObject go = col.gameObject;

		if (go.CompareTag (TagsManager.BOWL)) {
			StartCoroutine ("EnableGroundedTrigger");
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		GameObject go = col.gameObject;

		if (go.CompareTag (TagsManager.BOWL)) {
			isGrounded = false;
		}
	}

	IEnumerator EnableGroundedTrigger () {

		print ("Triggered!");
		yield return new WaitForSeconds (minWaitForGrounded);
		if (!isGrounded) {
			isGrounded = true;
		}


	}

	public void Move () {

		yVelocity += gameSettings.jumpForce.y;
		isGrounded = false;


	}
}
