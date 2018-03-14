using UnityEngine;

public class BowlMovement : MonoBehaviour {

	public float moveSpeed = 1f;
	public float moveLimit = 2.4f;

	private Vector2 translation;
	
	void Update () {
		
		if (transform.position.x > moveLimit) {
			moveSpeed = -Mathf.Abs (moveSpeed);
		} else if (transform.position.x < -moveLimit) {
			moveSpeed = Mathf.Abs (moveSpeed);
		}

		translation = new Vector2 (moveSpeed * Time.deltaTime, 0);
		transform.Translate (translation);

	}


}
