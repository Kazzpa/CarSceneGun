using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpSpeed : MonoBehaviour
{
    SphereCollider sc;
    MeshRenderer mr;
    PlayerMovement pm;
    public int points = 10;
    public AudioSource audio2;
    private void Start()
    {
        sc = GetComponent<SphereCollider>();
        mr = GetComponent<MeshRenderer>();
       // pm = GameObject.Find("Skycar").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        pm = other.GetComponent<PlayerMovement>();
        if (other.CompareTag("Player"))
        {
            audio2.Play();
            pm.Acellerate();
            pm.RemoveBonus(this);
            sc.enabled = false;
            mr.enabled = false;
            StartCoroutine(pm.AddPoints(points));
        }
    }

    
}
