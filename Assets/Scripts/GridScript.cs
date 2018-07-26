using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {
    public Transform grid;
    // Use this for initialization
    void Start()
    {
        grid = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        grid.Rotate(new Vector3(0, 0.6f, 0));
    }
}
