using UnityEngine;
using System.Collections;

public class MoveingPlatformController : MonoBehaviour {

	//PRIVATE INSTANCE VARABLES
	private Rigidbody2D _rigidbody;
	private bool _moveLeft;

	// Use this for initialization
	void Start () {
		this._initialisze ();
	}
	void FixedUpdate(){

		if (this._moveLeft) {
			this._rigidbody.AddForce (new Vector2 (-.1f, 0f));
		} else {
			this._rigidbody.AddForce (new Vector2 (.1f, 0f));
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnCollisionStay2D(Collision2D other){

		if(other.gameObject.CompareTag("Platform")){
			this._moveLeft = !this._moveLeft;
		}


	}


	//PRIVATE METHODS
	private void _initialisze(){

		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._moveLeft = true;
	}
}
