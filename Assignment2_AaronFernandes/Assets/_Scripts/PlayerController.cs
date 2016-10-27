using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*
 * Aaron Fernandes
 * 300773526
 * 
 * 2D Platformer
 * 
 */ 

/// <summary>
/// Player controller.
/// </summary>
public class PlayerController : MonoBehaviour {

	//PRIVATE INSTANCE VARABLES
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private float _move;
	private float _jump;
	private bool _isFaceingRight;
	private bool _isGrounded;
	private Vector2 SpawnPoint;
	private string[] _messages;

	//PUBLIC INSTAND VARABLES
	public float Velocity = 10f;
	public float JumpForce = 500f;
	public Camera camera;
	public Text MessageText;
	public GameObject ScoreBoard;

	[Header("Sound Clips")]
	public AudioSource OuchSound;
	public AudioSource TimeSound;
	public AudioSource DeadEnemySound;
	public AudioSource PlayerHurtSound;
	public AudioSource WarpSound;


	/// <summary>
	/// Fixed update for physics
	/// </summary>
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
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
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
	void Update () {}

	//PRIVATE METHODS


	/// <summary>
	/// Initialisze this instance.
	/// </summary>
	private void _initialisze(){
		this._transform = GetComponent<Transform> ();
		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._move = 0;
		this._isFaceingRight = true;
		this._isGrounded = false;



		this._messages= new string[] {"Warp ahead! just run into it, You'll be fine","Wait. Your platform will come soon!", "Almost there. Collect your space ship part and play again"};
	}

	/// <summary>
	/// Filips the charactors bitmat
	/// </summary>
	private void _flip(){
		if (this._isFaceingRight) {
			this._transform.localScale = new Vector2 (1f, 1f);
		} else {
			this._transform.localScale = new Vector2 (-1f, 1f);
		}
	}

	/// <summary>
	/// Raises the collision enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionEnter2D(Collision2D other){


		//player hits the spawn point
		if (other.gameObject.CompareTag ("DeathPlane")) {
			// move player to spawn point
 			this._transform.position = this.SpawnPoint;
			PlayerHurtSound.Play ();
		}

		if (other.gameObject.CompareTag ("Spike")) {
			OuchSound.Play ();
			ScoreBoard.GetComponent<ScoreboardController> ().Time -= 10f;
			ScoreBoard.GetComponent<ScoreboardController> ().Score -= 5f;

		}

		if (other.gameObject.CompareTag ("Enemy") && (other.GetType () == typeof(BoxCollider2D))) {
			PlayerHurtSound.Play ();
		}
	}

	/// <summary>
	/// Raises the collision stay2 d event
	/// for when the player hits the platform
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionStay2D(Collision2D other){

		if(other.gameObject.CompareTag("Platform")){
			this._isGrounded=true;
		}
		if(other.gameObject.CompareTag("Wrap")){
			WarpSound.Play ();
			this.transform.position = new Vector2(47f, 25f);
		}
	}


	/// <summary>
	/// Raises the collision exit2D to set isGrounded to false.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionExit2D(Collision2D other){this._isGrounded = false;}


	/// <summary>
	/// Sets the message to help the user
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Respawn")) {
			this.SpawnPoint = other.gameObject.GetComponent<Transform> ().position;
		}

		if (other.gameObject.CompareTag ("Message1")) {
			MessageText.text = this._messages [0];
		} else if (other.gameObject.CompareTag ("Message2")) {
			MessageText.text = this._messages [1];
		} else if (other.gameObject.CompareTag ("Message3")) {
			MessageText.text = this._messages [2];
		}

		if (other.gameObject.CompareTag ("Sand")) {
			TimeSound.Play ();
			Destroy (other.gameObject);
			ScoreBoard.GetComponent<ScoreboardController> ().Time += 10f;
			ScoreBoard.GetComponent<ScoreboardController> ().Score += 5f;
		}


		if (other.gameObject.CompareTag ("EndPart")) {
			Debug.Log ("HIT END");
			SceneManager.LoadScene ("Play");
		}

		if(other.gameObject.CompareTag("Enemy") && (other.GetType()  == typeof(BoxCollider2D))){
			DeadEnemySound.Play ();
			Destroy (other.gameObject);
			ScoreBoard.GetComponent<ScoreboardController> ().Score += 10f;
		}

	}


	/// <summary>
	/// Remove message when player exits
	/// the scope of the message
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag.StartsWith("Message")) {
			MessageText.text ="";
		}
	}

}
