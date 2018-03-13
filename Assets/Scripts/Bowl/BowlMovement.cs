using UnityEngine;

public class BowlMovement : MonoBehaviour {

	public float moveSpeed = 1f;

	public float moveLimit = 2.4f;

	private Vector2 translation;

	void Start () {
		
	}
	
	
	void Update () {
		
		if (Mathf.Abs (transform.position.x) > moveLimit) {
			moveSpeed = -moveSpeed;
		}
		translation = new Vector2 (moveSpeed * Time.deltaTime, 0);

		transform.Translate (translation);

	}

	void FixedUpdate () {
		
	}
}
