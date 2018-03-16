using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour {

	public float minLerpDistance = 0.01f;
	public Transform target;

	[SerializeField]
	private float offset;

	void Start () {
		target = GameObject.FindGameObjectWithTag (TagsManager.PLAYER).transform;
		offset = target.position.y - transform.position.y;
		//offset = target.position - transform.position;
	}


	void LateUpdate () {

		if ((transform.position.y + offset - target.position.y) > minLerpDistance) {
			transform.position = new Vector3 (transform.position.x,
				Mathf.Lerp (transform.position.y, target.position.y - offset, .4f),
				transform.position.z);
		} else {
			transform.position = new Vector3 (transform.position.x, target.position.y - offset, transform.position.z);

		}
	}



}
