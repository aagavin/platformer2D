using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//PRIVATE INSTANCE VARABLES
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private float _move;
	private float _jump;
	private bool _isFaceingRight;
	private bool _isGrounded;

	//PUBLIC INSTAND VARABLES
	public float Velocity = 10f;
	public float JumpForce = 300f;
	public Camera camera;
	public Transform SpawnPoint;

	//Fixed update for physics
	void FixedUpdate(){

		if (this._isGrounded) {
			//check for movement
			this._move = Input.GetAxis ("Horizontal");
			if (this._move > 0f) {
				this._move = 1;
				this._isFaceingRight = true;
				this._flip ();
			} else if (this._move < 0f) {
				this._move = -1;
				this._isFaceingRight = false;
				this._flip ();
			} else {
				this._move = 0;
			}
			//
			this._rigidbody.AddForce (new Vector2 (
				this._move * this.Velocity,
				this._jump * this.JumpForce),
				ForceMode2D.Force
			);

			// check for jump key
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
				this._jump = 1;
			}
		} else {
			this._move = 0f;
			this._jump = 0f;
		}

		this.camera.transform.position = new Vector3 (
			this._transform.position.x,
			this._transform.position.y,
			-6.989258f
		);
	}

	// Use this for initialization
	void Start () {
		this._initialisze ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//PRIVATE METHODS

	private void _initialisze(){
		this._transform = GetComponent<Transform> ();
		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._move = 0;
		this._isFaceingRight = true;
		this._isGrounded = false;
	}

	//filips the chars bitmay
	private void _flip(){
		if (this._isFaceingRight) {
			this._transform.localScale = new Vector2 (1f, 1f);
		} else {
			this._transform.localScale = new Vector2 (-1f, 1f);
		}
	}

	private void OnCollisionEnter2D(Collision2D other){
		//player hits the spawn point
		if (other.gameObject.CompareTag ("DeathPlane")) {
			// move player to spawn point
 			this._transform.position = this.SpawnPoint.position;
			Debug.Log (this.SpawnPoint.position);
		}
	}

	private void OnCollisionStay2D(Collision2D other){
		if(other.gameObject.CompareTag("Platform")){
			this._isGrounded=true;
		}
		if(other.gameObject.CompareTag("Wrap")){
			this.transform.position = new Vector2(47f, 25f);
		}
	}

	private void OnCollisionExit2D(Collision2D other){this._isGrounded = false;}

}
