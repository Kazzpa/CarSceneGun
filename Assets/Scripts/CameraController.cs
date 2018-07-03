
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player;
	public Vector3 offset;
    // Update is called once per frame
    void Update () {
		transform.position = player.position + offset;
	}
    public void RestartCamera()
    {
        transform.position = new Vector3(0, 4.56f, -56);
    }
}
