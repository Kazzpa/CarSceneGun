using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public PlayerMovement movement;
    public Rigidbody rb;
    public AudioSource audio;
    public float duration;
	// Use this for initialization
	void OnCollisionEnter (Collision col) {
		if (col.collider.tag == "Obstacle") {
            if (!audio.isPlaying)
            {
                audio.enabled = false;
            }
			Debug.Log (col.collider.tag + " hit");
            audio.enabled = true;
			movement.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
            rotate360();
		}
	}
    IEnumerator rotate360()
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
    }

}
