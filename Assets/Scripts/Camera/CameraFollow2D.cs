using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {

	public float minLerpDistance = 4f;
	public Transform target;

	private float offset;
	private float maxMergeDistance = .3f;
	public Vector3 startPos;

	void Start () {
		target = GameObject.FindGameObjectWithTag (TagsManager.PLAYER).transform;
		startPos = transform.position;
		offset = transform.position.y - target.position.y;
	}


	void LateUpdate () {

		if ((offset + target.position.y - transform.position.y) > minLerpDistance) {

			if ((offset + target.position.y - transform.position.y - minLerpDistance) > maxMergeDistance) {
				transform.Translate (Vector3.up * Mathf.Lerp (transform.position.y, target.position.y + offset - minLerpDistance, .2f) * Time.deltaTime);

			} else {
				transform.Translate (Vector3.up * (target.position.y + offset - minLerpDistance - transform.position.y));
			}
		} 
	}

	public void ResetPosition () {
		transform.position = startPos;
	}
		
}
