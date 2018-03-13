using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "GameSettings", order = 1)]
public class GameSettings : ScriptableObject {

	public int leftLife = 3;
	public Vector2 jumpForce = Vector2.up * 5f;
	//
}
