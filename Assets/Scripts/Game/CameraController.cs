
using Kino;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player;
	public float offsetVertical,offset2;
    private Vector3 offsetangled;
    public Vector3 velocity = new Vector3(5, 5, 5);
    private float angle = Mathf.PI / 2,rotation = 0;
    public DigitalGlitch glitch;
    // Update is called once per frame
    public void Start()
    {
        glitch = GetComponent<DigitalGlitch>();
        transform.position = player.position + new Vector3(0,offset2,offsetVertical);
    }
    public void FixedUpdate()
    {
        offsetangled = offsetVertical * player.forward;
        transform.position = player.position + offsetangled + new Vector3(0, offset2, 0);
    }
    public void Update()
    {
        
        transform.rotation = player.rotation;
    }
    public void RestartCamera()
    {
        transform.position = new Vector3(0, 4.56f, -56);
        transform.rotation = Quaternion.identity;
    }
    //Activates the glitch effect on the camera when it speeds.
    public void SpeedGlitch(bool b)
    {
        glitch.enabled = b;
    }
    public bool IsSpeedActive()
    {
        return glitch.enabled;
    }
    public void UpdateCameraAngle(float rotation)
    {
        this.rotation = rotation;
    }
}
