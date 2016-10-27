using UnityEngine;
using System.Collections;


/*
 * Aaron Fernandes
 * 300773526
 * 
 * 2D Platformer
 * 
 */ 

/// <summary>
/// Goblen enemy controller.
/// </summary>
public class GoblenEnemyController : MonoBehaviour {

	// private varables
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private bool _isGrounded;
	private bool _isGroundAhead;
	private bool _isPlayerDetected;

	// public 
	public float Speed = .01f;
	public float MaxSpeed = .11f;
	public Transform SightStart;
	public Transform SightEnd;
	public Transform LineOfSight;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {

		this._transform = GetComponent<Transform> ();
		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._isGrounded = false;
		this._isGroundAhead = true;
		this._isPlayerDetected = false;
	}
	
	/// <summary>
	/// Update is called once per frame for physics
	/// </summary>
	void FixedUpdate () {
		// check if the object is grounded
		if (this._isGrounded) {
			//
			this._rigidbody.velocity = new Vector2 (this._transform.localScale.x, 0) * this.Speed;


			this._isGroundAhead = Physics2D.Linecast (
				this.SightStart.position,
				this.SightEnd.position,
				1 << LayerMask.NameToLayer ("Solid")

			);

			this._isPlayerDetected = Physics2D.Linecast (
				this.SightStart.position,
				this.LineOfSight.position,
				1 << LayerMask.NameToLayer ("Player")

			);

			// DEBUG
			Debug.DrawLine(SightStart.position, SightEnd.position);
			Debug.DrawLine(SightStart.position, LineOfSight.position);

			if (this._isGroundAhead == false) {
				// flip the direction
				this._flip();
			}

			//check if player is dected and then increase speed
			if (this._isPlayerDetected) {
				//increse spped to 4
				this.Speed *=1.02f;
				if (this.Speed >= this.MaxSpeed) {
					this.Speed = this.MaxSpeed;
				}
			}
		}
	}


	/// <summary>
	/// filips the chars bitmay
	/// </summary>
	private void _flip(){
		if (this._transform.localScale.x ==2.748711f) {
			this._transform.localScale = new Vector3 (-2.748711f, 2.748711f, 2.748711f);
		} else {
			this._transform.localScale = new Vector3 (2.748711f, 2.748711f, 2.748711f);
		}
	}



	/// <summary>
	/// Raises the collision stay2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = true;
		}
	}


	/// <summary>
	/// Raises the collision exit2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = false;
		}
	}


	/// <summary>
	/// Raises the collision enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("Enemy")) {
			this._flip ();
		}
	}

}
