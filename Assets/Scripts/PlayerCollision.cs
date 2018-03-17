using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour {

	PlayerMovement movement;
    public AudioSource audio;
    float duration;
    float delayScene;
    float speed;
    float fin;
    bool collision;
    // Use this for initialization
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        delayScene = 0;
        fin = 2;
        speed = 1.4f;//MultiplyFactor
    }
    
    void OnCollisionEnter (Collision col) {
        //Collision with obstacle
		if (col.collider.tag.Equals("Obstacle")) {
            movement.enabled = false;
            audio.Play();
            rotate360();
            collision = true;

		}

	}
    private void Update()
    {
        //Endgame if collisioned
        if (collision)
        {
            delayScene += Time.deltaTime;
            if(delayScene > fin)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        //Quit game with ESCAPE
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
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
