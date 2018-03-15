using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody rb;
	private float ForceZ = 5;
	private float ForceX = 9;
	public float MaxZ;
	private float MinZ = 15;
    private float aux;
	private double h;
	private double v;
	public Text debug;
	// Use this for initialization
	void Start () {
		Debug.Log ("Ha empesao");
		debug.text = "Pulse A,S,D";
	}

	void Update(){
		h = Input.GetAxis ("Horizontal");	//Gets -1, 0,1
		v = Input.GetAxis( "Vertical");		//Gets -1 , 0 ,1
		//Vertical handler
		if (v == -1) {
			aux = ForceZ - 1;
			if (aux >= MinZ) {
				ForceZ = aux;
			} else {
				ForceZ = MinZ;
			}

		} else if(ForceZ < MaxZ){//Restoring speed
			aux =  ForceZ + (MaxZ - ForceZ ) * (float) 0.08;
			if (aux < MaxZ -0.1) {
				ForceZ = aux;
			} else {
				ForceZ = MaxZ;
			}
		}
		rb.velocity = new Vector3 (ForceX * (float)h, 0, ForceZ);
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
}
