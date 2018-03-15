using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlReset : MonoBehaviour {

	public void Reset () {
		GetComponent<BoxCollider2D> ().enabled = false;
		Transform trans = transform.GetChild (0);
		trans.gameObject.SetActive (true);
		trans.GetComponent <PolygonCollider2D> ().enabled = false;
		trans.GetComponent <BoxCollider2D> ().enabled = true;

	}

}
