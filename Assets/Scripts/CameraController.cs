
using Kino;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player;
	public Vector3 offset;
    public Vector3 velocity = new Vector3(5, 5, 5);
    public DigitalGlitch glitch;
    // Update is called once per frame
    public void Start()
    {
        glitch = GetComponent<DigitalGlitch>();
    }
    void Update () {
        //transform.position = Vector3.Lerp(transform.position, player.position + offset, 15f *Time.deltaTime);
        //transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref velocity, Time.deltaTime);
        //Fixed camera:
        //transform.position = player.position + offset;
	}
    private void FixedUpdate()
    {
        //Interpolating camera so it moves smoothly when the car speeds. and its shows a speed sensation ;)
        transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref velocity, Time.deltaTime);

    }
    public void RestartCamera()
    {
        transform.position = new Vector3(0, 4.56f, -56);
    }
    //Activates the glitch effect on the camera when it speeds.
    public void speedGlitch(bool b)
    {
        glitch.enabled = b;
    }
    public bool IsSpeedActive()
    {
        return glitch.enabled;
    }
}
