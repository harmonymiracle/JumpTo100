using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "GameSettings", order = 1)]
public class GameSettings : ScriptableObject {

	public const float JumpSqrtRoot = 6.57f;

	public int leftLife = 3;
	public Vector2 jumpForce = Vector2.up * JumpSqrtRoot;
	//
}
