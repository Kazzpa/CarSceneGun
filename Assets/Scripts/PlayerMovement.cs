using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody rb;
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
        k = Input.GetKeyDown("escape");     //returns true if pressed
        if (k)//restarts game if pressed
        {
            Debug.Log("Presionao escape");
            StartCoroutine(RestartGame(true));
        }
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
                Debug.Log("Aceleracion");
                rb.velocity = new Vector3(ForceX * (float)h, 0, ForceZ * speed);
                float auxspeed = (ForceZ * speed) * (120  / MaxZ);
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
        
	}
    public IEnumerator RestartGame(bool manual)
    {
        Debug.Log("Reinicio");
        collisioned = true;
        RestartUI();
        score = 0;
        ActivateBonus();
        if (!manual)
        {
            yield return new WaitForSeconds(2.8f);
        }
        RestartMovement();
        cc.RestartCamera();

    }
    public void Acellerate()
    {
        
        aceleracion = true;
        
    }
    public void RestartMovement()
    {
        transform.position = new Vector3(0, 0.56f, -46);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        ForceZ = MinZ;
        aceleracion = false;
        collisioned = false;

    }
    public void RestartUI()
    {
        debug.SetText("Pulse A,S,D");
        UISpeed.SetText("Speed 0");
        UIScore.SetText("Score: 0");
        UIScoreAlert.enabled = false;
        speedEffect.SetActive(false);
    }
    public IEnumerator AddPoints(int points)
    {

        score += points;
        UIScore.SetText("Score: "+score);
        UIScoreAlert.enabled = true;
        yield return new WaitForSeconds(0.3f);
        UIScoreAlert.enabled = false;
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
