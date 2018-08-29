using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Suelo
public class PowerUpSpeed : MonoBehaviour
{
    BoxCollider sc;
    MeshRenderer mr;
    public int points = 10;
    public AudioSource audio2;
    private void Start()
    {
        sc = GetComponent<BoxCollider>();
        mr = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {

        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("Aceleracion objeto");
            audio2.Play();
            pm.Acellerate();
            pm.RemoveBonus(this);
            sc.enabled = false;
            mr.enabled = false;
            StartCoroutine(pm.AddPoints(points));
        }
    }

    
}
