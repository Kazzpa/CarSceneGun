using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//BOTELLA
public class PowerUpBoost : MonoBehaviour {

    SphereCollider bc;
    MeshRenderer mr;
    private void Start()
    {
        bc = GetComponent<SphereCollider>();
        mr = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {

        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (other.CompareTag("Player"))
        {
            //Disables the box collider and the renderer if its added.
            if (pm.AddNitro(this))
            {
                bc.enabled = false;
                mr.enabled = false;
            }
            else
            {
                Debug.Log("No puede añadir el boost debido a que ya esta en el limite");
            }
        }
    }
}
