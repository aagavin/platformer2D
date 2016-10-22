using UnityEngine;
using System.Collections;

public class MoveingPlatformController : MonoBehaviour {

	//PRIVATE INSTANCE VARABLES
	private Rigidbody2D _rigidbody2D;
	private bool _moveLeft;

	// Use this for initialization
	void Start () {
		this._initialisze ();
	}
	void FixedUpdate(){

		if (this._moveLeft) {
			this._rigidbody2D.MovePosition(new Vector2(this._rigidbody2D.position.x-.1f, this._rigidbody2D.position.y+0f));
		} else {
			this._rigidbody2D.MovePosition(new Vector2 (this._rigidbody2D.position.x+.1f, this._rigidbody2D.position.y+0f));
		}

		Debug.Log (this._rigidbody2D.position);
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.CompareTag("Platform")){
			Debug.Log ("HIT");
			this._moveLeft = !this._moveLeft;
		}


	}


	//PRIVATE METHODS
	private void _initialisze(){

		this._rigidbody2D = this.GetComponent<Rigidbody2D> ();
		this._moveLeft = true;
	}
}
