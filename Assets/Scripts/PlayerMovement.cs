using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody rb;
    public CameraController cc;
	private float ForceZ = 5, ForceX = 9;
	private float MaxZ = 30, MinZ = 15;
    private float aux, t,tr, speed = 10f;
    private float maxSpeed = 10f;
    private double h, v;
    private bool c, k, Acelleration, collisioned,first;
    private int score = 0;
    List<PowerUpSpeed> bonus;
    List<PowerUpBoost> boost, boostBackup;
    public TextMeshProUGUI debug, UISpeed, UIBoost, UIScore, UIScoreAlert;
    // Use this for initialization
    void Start () {
        Debug.Log("Start");
        first = true;
        bonus = new List<PowerUpSpeed>();
        boost = new List<PowerUpBoost>();
        boostBackup = new List<PowerUpBoost>();
        rb = GetComponent<Rigidbody>();
		debug.SetText("Pulse A,S,D");
        UISpeed.SetText("Speed 0");
        UIScore.SetText("Score: 0");
        UIScoreAlert.enabled = false;
        UIBoost.enabled = false;
        cc.speedGlitch(false);
    }
    //Handles the speed applied to the Car, and the scene restart.
    void Update() {
        h = Input.GetAxis("Horizontal");    //Gets -1, 0,1
        v = Input.GetAxis("Vertical");      //Gets -1 , 0 ,1
        k = Input.GetKeyDown("escape");     //returns true if pressed
        c = Input.GetKeyDown("mouse 0");
        //Pressed Escape->Restarts the game.
        if (k)
        {
            Debug.Log("Presionao escape");
            StartCoroutine(RestartGame(true));
        }
        //Clicked left mouse ->uses nitro
        if (c)
        {
            StartCoroutine(UseNitro());
        }
        //If collisioned it doesn't move.
        if (!collisioned) { 
        
            t = 0;
            //Vertical handler:
            //Slowing
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
            //Restoring speed to max
            else if (ForceZ < MaxZ)
            {
                    if (ForceZ < MaxZ)
                    {
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
             }
            //Restores speed if recently accellerated
            else
            {
                    aux = ForceZ - 10;
                    if (aux >= MaxZ)
                    {
                        ForceZ = aux;
                    }
                    else
                    {
                        ForceZ = MaxZ;
                    }
             }
            if (Acelleration)
            {
                //Acelleration starts
                if (first)
                {
                    tr = Time.realtimeSinceStartup;
                    first = false;
                    cc.speedGlitch(true);
                }
                t = Time.realtimeSinceStartup;
                //Acelleration in process
                if (t-tr < 1.5f)
                {
                    rb.velocity = new Vector3(ForceX * (float)h, 0, ForceZ * speed);
                    float auxspeed = (ForceZ * speed) * (120 / 30);
                    UISpeed.SetText("Speed " + auxspeed.ToString("0.0"));
                    if (speed > 1)
                    {
                        speed -= 0.5f;
                    }
                }
                //Acelleration ends
                else
                {
                    cc.speedGlitch(false);
                    Debug.Log("Efecto aceleracion " + cc.IsSpeedActive());
                    Acelleration = false;
                    first = true;
                    speed = maxSpeed;
                }
            }
            // Applies the speed corresponding
            else
            {
                rb.velocity = new Vector3(ForceX * (float)h, 0, ForceZ);
                float auxspeed = ForceZ * (120 / 30);
                UISpeed.SetText("Speed " + auxspeed.ToString("0.0"));
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
    //Actives acceleration, called from outside
    public void Acellerate()
    {
        first = true;
        Acelleration = true;
    }
    //restart the speed,rotation,and force applied to it
    public void RestartMovement()
    {
        transform.position = new Vector3(0, 0.56f, -46);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        ForceZ = 0;
        first = true;
        Acelleration = false;
        collisioned = false;

    }
    //Restart the UI as when the game Starts.
    public void RestartUI()
    {
        debug.SetText("Pulse A,S,D");
        UISpeed.SetText("Speed 0");
        UIScore.SetText("Score: 0");
        UIScoreAlert.enabled = false;
        cc.speedGlitch(false);
    }
    //Restore the bonuses used
    public void RestoreBonus()
    {
        Debug.Log("Activar bonuses");
        foreach (PowerUpSpeed g in bonus)
        {
            g.GetComponent<BoxCollider>().enabled = true;
            g.GetComponent<MeshRenderer>().enabled = true;
        }
        foreach (PowerUpBoost g in boost)
        {
            g.GetComponent<SphereCollider>().enabled = true;
            g.GetComponent<MeshRenderer>().enabled = true;
        }
        foreach (PowerUpBoost g in boostBackup)
        {
            g.GetComponent<SphereCollider>().enabled = true;
            g.GetComponent<MeshRenderer>().enabled = true;
        }
        bonus.Clear();
        boost.Clear();
        boostBackup.Clear();
    }
    //RestartsThe game
    public IEnumerator RestartGame(bool manual)
    {
        Debug.Log("Reinicio");
        RestartUI();
        score = 0;
        RestoreBonus();
        if (!manual)
        {
            yield return new WaitForSeconds(2.8f);
        }
        StopAllCoroutines();
        RestartMovement();
        cc.RestartCamera();
        UIBoost.enabled = false;
    }
    //Called from outside set the points on the ui,and on the score
    public IEnumerator AddPoints(int points)
    {

        score += points;
        UIScore.SetText("Score: "+score);
        UIScoreAlert.enabled = true;
        yield return new WaitForSeconds(0.4f);
        UIScoreAlert.enabled = false;
    }
    //Adds the bonus to the list so it can be restored,called from outside
    public void RemoveBonus(PowerUpSpeed o)
    {
        bonus.Add(o);
    }
    //Adds the boost to the list so it can be used and restore, called from outside
    public void AddNitro(PowerUpBoost p)
    {
        boost.Add(p);
        if(UIBoost.enabled == false)
        {
            UIBoost.enabled = true;
        }
        UIBoost.SetText(boost.Count.ToString());
    }
    //Method to acellerate actively the speed
    public IEnumerator UseNitro()
    {
        if(boost.Count!= 0)
        {
            boostBackup.Add(boost[0]); 
            boost.RemoveAt(0);
            float aux = MaxZ;
            MaxZ = MaxZ * 5f;
            yield return new WaitForSeconds(0.5f);
            MaxZ = aux;
            UIBoost.SetText(boost.Count.ToString());
        }
        if(boost.Count == 0)
        {
            UIBoost.enabled = false;
        }
    }
}
