using UnityEngine;

public class CameraScript : MonoBehaviour
{
	private Camera camera;
	public GameObject player;
	public float min = 4f;
	public float max = 16f;
	void Start()
	{
		camera = GetComponent<Camera>();
	}

	void Update()
	{
		var mousePlus = -Input.GetAxis("Mouse ScrollWheel");
		camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -11);
		if (mousePlus == 0)
			return;
		if (mousePlus > 0 && camera.orthographicSize <= max)
			camera.orthographicSize = camera.orthographicSize + mousePlus * 4;
		if (mousePlus < 0 && camera.orthographicSize >= min)
			camera.orthographicSize = camera.orthographicSize + mousePlus * 4;
	}
}
