using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody rb;
	private float ForceZ = 5;
	private float ForceX = 9;
	public float MaxZ;
	private float MinZ = 15;
    private float aux;
    private float t;
    private float speed = 10f;
    private float maxSpeed = 10f;
	private double h;
	private double v;
    private bool aceleracion;
    public GameObject speedEffect;
	public Text debug;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		Debug.Log ("Ha empesao");
		debug.text = "Pulse A,S,D";
        speedEffect.SetActive(false);
    }

	void Update(){
            h = Input.GetAxis("Horizontal");    //Gets -1, 0,1
            v = Input.GetAxis("Vertical");      //Gets -1 , 0 ,1
        
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


        }else{
            
            t += Time.deltaTime;
            speedEffect.SetActive(true);
            
            if (t < 1)
            {

                rb.velocity = new Vector3(ForceX * (float)h , 0, ForceZ * speed);
                if (speed > 1)
                {
                    speed -= 0.5f;
                }
                Debug.Log(speed);                
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
            debug.text = "Derecha";
        }
        else if(h <0)
        {
            debug.text = "Izquierda";
        }
        else if( v < 0)
        {
            debug.text = "Frenando";
        }
        else
        {
            debug.text = "Presione A,S,D";
        }

	}
    public void Acellerate()
    {
        
        aceleracion = true;
        
    }
}
