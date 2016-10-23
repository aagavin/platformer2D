using UnityEngine;
using System.Collections;

public class GoblenEnemyController : MonoBehaviour {

	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private bool _isGrounded;
	private bool _isGroundAhead;

	// public 
	public float Speed = .01f;
	public Transform SightStart;
	public Transform SightEnd;

	// Use this for initialization
	void Start () {

		this._transform = GetComponent<Transform> ();
		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._isGrounded = false;
		this._isGroundAhead = true;
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

			// DEBUG
			Debug.DrawLine(SightStart.position, SightEnd.position);

			if (this._isGroundAhead == false) {
				// flip the direction
				this._flip();
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

	private void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = false;
		}
	}


	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("Enemy")) {
			this._flip ();
		}
	}
}
