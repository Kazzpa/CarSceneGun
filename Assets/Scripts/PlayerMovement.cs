using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody rb;
	public float ForceZ;
	public float ForceX;
	// Use this for initialization
	void Start () {
		Debug.Log ("Ha empesao");
	}
	
	// FixedUpdate for physics
	void FixedUpdate () {
		//velocidad fija
		rb.AddForce (0, 0, ForceZ * Time.deltaTime);
		//Movimiento lateral
		if( Input.GetKey("d") ){
			rb.AddForce (ForceX * Time.deltaTime, 0, 0);
			
		}else if( Input.GetKey("a") ) {

			rb.AddForce (- ForceX * Time.deltaTime, 0, 0);
		}
	}
}
