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
/// Moveing platform controller.
/// Handles the moving of the platform
/// </summary>
public class MoveingPlatformController : MonoBehaviour {

	//PRIVATE INSTANCE VARABLES
	private Rigidbody2D _rigidbody2D;
	private bool _moveLeft;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
		this._initialisze ();
	}

	/// <summary>
	/// Physics update
	/// </summary>
	void FixedUpdate(){

		if (this._moveLeft) {
			this._rigidbody2D.MovePosition(new Vector2(this._rigidbody2D.position.x-.05f, this._rigidbody2D.position.y+0f));
		} else {
			this._rigidbody2D.MovePosition(new Vector2 (this._rigidbody2D.position.x+.05f, this._rigidbody2D.position.y+0f));
		}

	}
	
	/// <summary>
	///  Update is called once per frame
	/// </summary>
	void Update () {}


	/// <summary>
	/// Raises the collision enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.CompareTag("Platform")){
			this._moveLeft = !this._moveLeft;
		}


	}


	//PRIVATE METHODS

	/// <summary>
	/// Initialisze at start.
	/// </summary>
	private void _initialisze(){
		this._rigidbody2D = this.GetComponent<Rigidbody2D> ();
		this._moveLeft = true;
	}
}
