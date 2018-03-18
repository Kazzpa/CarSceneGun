using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    SphereCollider sc;
    MeshRenderer mr;
    PlayerMovement pm;
    public AudioSource audio2;
    float speed  = 10f;
    private void Start()
    {
        sc = GetComponent<SphereCollider>();
        mr = GetComponent<MeshRenderer>();
       // pm = GameObject.Find("Skycar").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Choque");
        pm = other.GetComponent<PlayerMovement>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ifaso");
            audio2.Play();
            pm.Acellerate();
            sc.enabled = false;
            mr.enabled = false;
        }
    }

    
}
