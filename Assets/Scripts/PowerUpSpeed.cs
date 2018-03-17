using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    Rigidbody rb;
    SphereCollider sc;
    MeshRenderer mr;
    PlayerMovement pm;
    public AudioSource audio2;
    float speed  = 1.4f;
    private void Start()
    {
        sc = GetComponent<SphereCollider>();
        mr = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Choque");
        rb = other.GetComponent<Rigidbody>();
        pm = other.GetComponent<PlayerMovement>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ifaso");
            audio2.Play();
            pm.acellerate(speed);
            sc.enabled = false;
            mr.enabled = false;
        }
    }

    
}
