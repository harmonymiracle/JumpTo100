using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour {

	public GameSettings gameSettings;
	public float maxGroundedDistance = .4f;

	private Rigidbody2D rb2d;
	private bool isGrounded = true;
	private Transform myTransform;
	private GameManager gm;
	private bool hasJumped = false;



	void Awake () {
		myTransform = transform;
		rb2d = GetComponent <Rigidbody2D> ();
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



	void OnTriggerEnter2D (Collider2D col) {
		GameObject go = col.gameObject;

		switch (go.tag) {

		case TagsManager.BOWL:
			isGrounded = true;
			if (hasJumped) {
				hasJumped = false;

				gm.LandingOver (go.transform, rb2d.velocity.y < 0);

				bool destroyTheTrigger = gm.LandingOver (go.transform, rb2d.velocity.y < 0);


				if (destroyTheTrigger) {
					go.GetComponent<BoxCollider2D> ().enabled = false;
				}
			}
			break;

		case TagsManager.COLLISION:
			gm.SetUpBeforeFall (go);
			break;
		}
	}


	public void Move () {

		if (isGrounded) {
			rb2d.AddForce (gameSettings.jumpForce, ForceMode2D.Impulse);
			isGrounded = false;
			hasJumped = true;
			gm.LaunchSetting ();
		}
	}
}
