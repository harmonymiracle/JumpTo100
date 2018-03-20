using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {

	public float minLerpDistance = 4f;
	public Transform target;

	private Vector3 offset = new Vector3 (0f, 3.8f, -10f);
	private float maxMergeDistance = .3f;

	public void Awake () {
		target = GameObject.FindGameObjectWithTag (TagsManager.PLAYER).transform;
		offset = transform.position - target.position;
	}

	void LateUpdate () {

		if ((offset.y + target.position.y - transform.position.y) > minLerpDistance) {

			if ((offset.y + target.position.y - transform.position.y - minLerpDistance) > maxMergeDistance) {
				transform.Translate (Vector3.up * Mathf.Lerp (transform.position.y, target.position.y + offset.y - minLerpDistance, .2f) * Time.deltaTime);

			} else {
				transform.Translate (Vector3.up * (target.position.y + offset.y - minLerpDistance - transform.position.y));
			}
		} 
	}

	public void ResetPosition () {
		Vector3 temp = target.position + offset;
		// 防止 视界出现左右移动
		transform.position = new Vector3 (0f, temp.y, temp.z);
	}
		
}
