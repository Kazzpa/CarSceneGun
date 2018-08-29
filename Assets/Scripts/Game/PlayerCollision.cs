using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour {

	PlayerMovement movement;
    AudioSource hit;
    float duration;
    // Use this for initialization
    private void Start()
    {
        hit = GetComponent<AudioSource>();
        movement = GetComponent<PlayerMovement>();
        duration = 3;
    }
    //Collision with obstacle
    void OnCollisionEnter (Collision col) {
		if (col.collider.tag.Equals("Obstacle")) {
            Debug.Log("Chocaste con un obstaculo");
            //movement.enabled = false;
            hit.Play();
            Rotate360();
            StartCoroutine(movement.RestartGame(false));
		}

	}
    IEnumerator Rotate360()
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
