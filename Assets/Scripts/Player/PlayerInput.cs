using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


/// <summary>
/// Here we just use the getMouseButtonDown to replace Input.GetTouch for convenience
/// </summary>
[RequireComponent (typeof(PlayerMove))]
public class PlayerInput : MonoBehaviour {


	private PlayerMove playerMove;


	void Awake () {
		playerMove = GetComponent<PlayerMove> ();

	}
	
	void Update () {
		CheckInput ();
	}


	void CheckInput () {
		if (Input.GetMouseButtonDown (0)) {
			playerMove.Move ();
		}
	}



}
