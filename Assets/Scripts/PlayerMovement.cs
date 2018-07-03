using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody rb;
    public CameraController cc;
	private float ForceZ = 5;
	private float ForceX = 9;
	public float MaxZ = 30;
	private float MinZ = 15;
    private float aux;
    private float t;
    private float speed = 10f;
    private float maxSpeed = 10f;
	private double h;
	private double v;
    private bool k;
    private int score = 0;
    private bool aceleracion;
    private bool collisioned;
    List<PowerUpSpeed> bonus;
    public GameObject speedEffect;
	public TextMeshPro debug;
    public TextMeshPro UISpeed;
    public TextMeshPro UIScore;
    public TextMeshPro UIScoreAlert;
    // Use this for initialization
    void Start () {
        Debug.Log("Start");
        bonus = new List<PowerUpSpeed>();
        rb = GetComponent<Rigidbody>();
		debug.SetText("Pulse A,S,D");
        UISpeed.SetText("Speed 0");
        UIScore.SetText("Score: 0");
        UIScoreAlert.enabled = false;
        speedEffect.SetActive(false);
    }

    void Update() {
        h = Input.GetAxis("Horizontal");    //Gets -1, 0,1
        v = Input.GetAxis("Vertical");      //Gets -1 , 0 ,1
        k = Input.GetKey("escape");     //returns true if pressed
        if (!collisioned) { 
        if (!aceleracion)
        {
            t = 0;
            speed = maxSpeed;
            //Vertical handler
            if (v == -1)
            {
                aux = ForceZ - 1;
                if (aux >= MinZ)
                {
                    ForceZ = aux;
                }
                else
                {
                    ForceZ = MinZ;
                }

            }
            else if (ForceZ < MaxZ)
            {//Restoring speed
                aux = ForceZ + (MaxZ - ForceZ) * 0.08f;
                if (aux < MaxZ - 0.1)
                {
                    ForceZ = aux;
                }
                else
                {
                    ForceZ = MaxZ;
                }
            }
            rb.velocity = new Vector3(ForceX * (float)h, 0, ForceZ);
            float auxspeed = ForceZ * (120 / MaxZ);
            UISpeed.SetText("Speed " + auxspeed.ToString("0.0"));


        }
        else {

            t += Time.deltaTime;
            speedEffect.SetActive(true);

            if (t < 1)
            {

                rb.velocity = new Vector3(ForceX * (float)h, 0, ForceZ * speed);
                float auxspeed = ForceZ * ((120 + speed) / MaxZ);
                UISpeed.SetText("Speed " + auxspeed.ToString("0.0"));
                if (speed > 1)
                {
                    speed -= 0.5f;
                }
            }
            else
            {

                speedEffect.SetActive(false);
                Debug.Log("Hola " + speedEffect.activeSelf);
                aceleracion = false;
            }

        }
        //DEBUG TEXT
        if (h > 0)
        {
            debug.SetText("Derecha");
        }
        else if (h < 0)
        {
            debug.SetText("Izquierda");
        }
        else if (v < 0)
        {
            debug.SetText("Frenando");
        }
        else
        {
            debug.SetText("Presione A,S,D");
        }
        }
        if (k)
        {
            RestartGame();
        }
	}
    public void RestartGame()
    {
        Debug.Log("Reinicio");
        collisioned = true;
        debug.SetText("Pulse A,S,D");
        UISpeed.SetText("Speed 0");
        UIScore.SetText("Score: 0");
        UIScoreAlert.enabled = false;
        speedEffect.SetActive(false);
        score = 0;
        rb.position = new Vector3(0, 0.56f, -46);
        cc.RestartCamera();
        ActivateBonus();

    }
    public void Acellerate()
    {
        
        aceleracion = true;
        
    }
    public void AddPoints(int points)
    {

        score += points;
        UIScore.SetText("Score: "+score);
        UIScoreAlert.enabled = true;

        //UIScoreAlert.enabled = false;
    }
    public void RemoveBonus(PowerUpSpeed o)
    {
        Debug.Log("añadido bonus");
        bonus.Add(o);
    }
    public void ActivateBonus()
    {
        Debug.Log("Activar bonuses");
        foreach(PowerUpSpeed g in bonus)
        {
            g.GetComponent<SphereCollider>().enabled = true;
            g.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
