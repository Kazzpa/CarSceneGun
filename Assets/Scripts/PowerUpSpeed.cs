using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    public GameObject sphere;
    Rigidbody rb;
    public AudioSource audio2;
    float speed  = 1.4f;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Choque");
        rb = col.GetComponent<Rigidbody>();
        if (col.tag.Equals("Player"))
        {
            audio2.Play();
            acellerate(speed);
        }
    }

    public void acellerate(float speed)
    {
        Debug.Log("Aceleracion");
        float t = Time.deltaTime;
        while (t < 3)
        {
            t = Time.deltaTime;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z * speed);
        }
        Destroy(sphere);
    }
}
