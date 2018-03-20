using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "GameSettings", order = 1)]
public class GameSettings : ScriptableObject {

	public GameDifficulty gameDifficulty;

	public const float JumpSqrtRoot = 6.57f;
	public const float SCREEN_HORIZONTAL_LIMIT = 4.4f;
	public float speedLimt = 3.5f;
	public int leftLife = 3;
	public float screenHorizontalLimit = SCREEN_HORIZONTAL_LIMIT;
	public Vector2 jumpForce = Vector2.up * JumpSqrtRoot;
	public float soundVolume = .5f;
	public int highestLevelHistory = 0;
	public int currentLevel = 0;

	//
}
