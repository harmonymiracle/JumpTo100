using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
	Idle,
	Jumping,
	Falling
}

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerStateController : MonoBehaviour {

	private PlayerState playerState = PlayerState.Idle;

	public PlayerState PlayerState {
		get { return playerState; }
	}

	private Rigidbody2D rb2d;

	void Awake () {
		rb2d = GetComponent <Rigidbody2D> ();
	}
		
	void FixedUpdate () {
		if (rb2d.velocity.y == 0f) {
			playerState = PlayerState.Idle;
		} else if (rb2d.velocity.y > 0f) {
			playerState = PlayerState.Jumping;
		} else if (rb2d.velocity.y < 0f) {
			playerState = PlayerState.Falling;
		}
	}

}
