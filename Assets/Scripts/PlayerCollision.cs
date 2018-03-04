using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public PlayerMovement movement;
	// Use this for initialization
	void OnCollisionEnter (Collision col) {
		if (col.collider.tag == "Obstacle") {
			
			Debug.Log (col.collider.tag + " hit");
			movement.enabled = false;
		}
	}

}
