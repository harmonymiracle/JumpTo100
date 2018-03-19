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

	private Transform myTransform;
	private GameManager gm;

	private float yVelocity = 0;
	private bool isGrounded = true;
	private Rigidbody2D rb2d;
	public bool IsGrounded {
		get { return isGrounded; }
		set { isGrounded = true; }
	}

	void Awake () {
		myTransform = transform;
		gm = FindObjectOfType <GameManager> ();
		rb2d = GetComponent <Rigidbody2D> ();
	}

	void FixedUpdate () {

		if (!isGrounded) {
			// 应用模拟重力
			yVelocity += Time.fixedDeltaTime * Physics2D.gravity.y * gravityScale;

			transform.Translate (Vector2.up * yVelocity * Time.deltaTime);
		} else {
			yVelocity = 0;
		}
	}


	public void Move () {
		if (isGrounded) {

			// 避免当前level的速度对Player造成影响
			transform.SetParent (empty);

			gm.LaunchSetting ();
			yVelocity += gameSettings.jumpForce.y;
			isGrounded = false;
		}
	}

	public void Reset () {
		yVelocity = 0;
		isGrounded = true;
	}

}
