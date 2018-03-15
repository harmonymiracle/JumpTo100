using UnityEngine;


// 此处添加 一些对于bowl 对象的说明
// bowl对象 本身添加有一个 BoxCollider2D 对象，这个是用于检测 
// 球是否已正确落入碗中，以及球是否可以起跳
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
