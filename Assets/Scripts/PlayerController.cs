using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax;
}

public class PlayerController : MonoBehaviour
{

	private float maxSpeed = 3f;
	private float jumpForce = 650;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public Boundary boundary;
	//private float verticalSpeed = 10;

	[HideInInspector]
	public GameObject Boost;
	[HideInInspector]
	public GameObject Cloud;

	private Rigidbody2D rb2d;
	private Animator anim;
	private bool isGrounded = false;

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		Cloud = GameObject.Find("Cloud");
	}

	void OnCollisionEnter2D(Collision2D collision2D)
	{
		// the difference between the two objects velocities BEFORE colliding
		// Create the colliding cloude 
		if (collision2D.relativeVelocity.magnitude > 15)
		{
			Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
		}

		if (collision2D.gameObject.CompareTag("obstacle"))
		{
			Debug.Log("Hit");
			GameController.gameOver = true;
		}
	}

	// Update is called once per frame
	void Update()
	{
		// Check the jump action 
		if (Input.GetButtonDown("Jump") && (isGrounded))
			rb2d.AddForce(new Vector2(0, jumpForce));
	}

	void FixedUpdate()
	{
		//Horizontal Movement
		float hor = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(hor));
		rb2d.velocity = new Vector2(hor * maxSpeed, rb2d.velocity.y);

		//Check Ground 
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15F, whatIsGround);
		anim.SetBool("IsGrounded", isGrounded);

		//Boundray 
		rb2d.position = new Vector3
		(
			Mathf.Clamp(rb2d.position.x, boundary.xMin, boundary.xMax),
			transform.position.y,
			0.0f
		);
	}
}
