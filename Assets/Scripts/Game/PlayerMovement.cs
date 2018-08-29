using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody rb;
    public CameraController cc;
    public BackupScript bs;
    public CanvasHandler ch;
    private Vector3 InitialPosition;
	private float ForceZ = 5, ForceX = 9;
	private float MaxZ = 30, MinZ = 15;
    private float aux, t,tr, speed = 10f;
    private float maxSpeed = 10f;
    private double h, v;
    private bool c, k, Acelleration, collisioned,first;
    private int score = 0;
    // Use this for initialization
    void Start () {
        Debug.Log("Start");
        first = true;
        rb = GetComponent<Rigidbody>();
        InitialPosition = transform.position;
        /**
        UIScore.SetText("Score: 0");
        UIScoreAlert.enabled = false;
        UIBoost.enabled = false;**/
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
            Debug.Log("Restart Game");
            StartCoroutine(RestartGame(true));
        }
        //Clicked left mouse ->uses nitro
        if (c)
        {
            bs.UseNitro();
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
                //This is used to reduce drastically the speed
                if(ForceZ > MaxZ + 100)
                {
                    ForceZ -= 100;
                }
                    aux = ForceZ - 12;
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
                    ch.UpdateSpeed(ForceZ * speed);
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
                ch.UpdateSpeed(ForceZ);
            }

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
        transform.position = InitialPosition;
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
        ch.UpdateSpeed(0);
        ch.UpdateScore(score);
        ch.ActivateScoreAlert(false);
        cc.speedGlitch(false);
    }
    //Restarts game
    public IEnumerator RestartGame(bool manual)
    {
        Debug.Log("Reinicio");
        score = 0;
        RestartUI();
        //RestoreBonus();
        bs.RestoreAll();
        if (!manual)
        {
            yield return new WaitForSeconds(1.3f);
        }
        else
        {
            yield return new WaitForSeconds(0.7f);
        }
        StopAllCoroutines();
        RestartMovement();
        cc.RestartCamera();
        ch.ActivateBoost(false);
        
    }
    //Called from outside set the points on the ui,and on the score
    public IEnumerator AddPoints(int points)
    {

        score += points;
        ch.UpdateScore(score);
        ch.ActivateScoreAlert(true);
        yield return new WaitForSeconds(0.4f);
        ch.ActivateScoreAlert(false);
    }
    //Adds the bonus to the list so it can be restored --DONE
    public void RemoveBonus(PowerUpSpeed o)
    {
        bs.BackUpBonus(o);
    }
    //Adds the boost to the list so it can be used and restore --DONE
    public bool AddNitro(PowerUpBoost p)
    {
        if (bs.BackupBoost(p))
        {
            if (!ch.IsBoostActive())
            {
                ch.ActivateBoost(true);
            }
            ch.UpdateBoost(bs.BoostCount());
            return true;
        }
        return false;
    }
    //Maximizes the maximum speed
    public IEnumerator UseNitro()
    {
        float aux = MaxZ;
        MaxZ = MaxZ * 1.2f;
        yield return new WaitForSeconds(1.2f);
        MaxZ = aux;
    }

}
