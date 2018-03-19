using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Here we just use the getMouseButtonDown to replace Input.GetTouch for convenience
/// </summary>
[RequireComponent (typeof(PlayerMove))]
public class PlayerInput : MonoBehaviour {

	public float distance = 10000f;

	private PlayerMove playerMove;
	private GameManager gm;

	void Awake () {
		playerMove = GetComponent<PlayerMove> ();
		gm = FindObjectOfType <GameManager> ();
	}
	
	void Update () {
		CheckInput ();
	}


	void CheckInput () {

		if (Input.GetMouseButtonDown (0)) {

			// 当不是点击在 UI上时
			if (! EventSystem.current.IsPointerOverGameObject ()) {
				if (gm.IsInputActive) {
					playerMove.Move ();
				}
			}

		}
	}



}
