using UnityEngine;

public class CameraScript : MonoBehaviour
{
	private Camera cam;
	public GameObject player;
	public float min;
	public float max;
	void Start()
	{
		cam = GetComponent<Camera>();
	}

	void Update()
	{
		var mousePlus = -Input.GetAxis("Mouse ScrollWheel");
		cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -11);
		if (mousePlus == 0)
			return;
		if (mousePlus > 0 && cam.orthographicSize <= max)
			cam.orthographicSize = cam.orthographicSize + mousePlus * 3;
		if (mousePlus < 0 && cam.orthographicSize >= min)
			cam.orthographicSize = cam.orthographicSize + mousePlus * 3;
	}
}
