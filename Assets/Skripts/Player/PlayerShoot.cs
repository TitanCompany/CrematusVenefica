using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	public Camera cam;
	public Rigidbody2D rbody;

	public Transform firePoint;
	public GameObject arrowPrefab;
	private float angle;
	private Transform player;
	public int numArrows;
	public int maxArrows;

	public float arrowForce = 20f;
	private float shootRate = 2f;
	private float nextShootTime = 0f;

	float timer = 0;

	Vector2 mousePos;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		numArrows = maxArrows;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
			Vector2 lookDir = mousePos - rbody.position;
			angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
			print(angle+"\n"+player.localScale.x+" "+ player.localScale.y + " "+ player.localScale.z);
			if (timer>0.15f)
				if(((angle > -180 && angle < 0) && player.localScale.x>0) || ((angle>0||angle<-180)&&player.localScale.x<0))
					Shoot();
		}

		timer += Time.deltaTime;
	}

	private void Shoot()
	{
		if (numArrows == 0)
			return;
		firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
		GameObject bullet = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

		Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
		if(player.localScale.x>0)
			rbody.AddForce(firePoint.up * arrowForce, ForceMode2D.Impulse);
		if(player.localScale.x<0)
			rbody.AddForce(firePoint.up * arrowForce * -1, ForceMode2D.Impulse);
		firePoint.Rotate(0f, 0f, -angle);
		numArrows--;
		timer = 0;
	}
}
