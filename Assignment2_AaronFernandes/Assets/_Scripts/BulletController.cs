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
/// Bullet controller.
/// </summary>
public class BulletController : MonoBehaviour {

	private Vector2 _startPosition;
	private Transform _transform;
	private Transform _playerTransform;
	private bool _isFireing;


	//
	public GameObject Player;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
		this._playerTransform = Player.gameObject.GetComponent<Transform> ();
		this._startPosition = new Vector2 ((this._playerTransform.position.x) + 0.12f, this._playerTransform.position.y + 0.06200004f);
		this._transform = GetComponent<Transform> ();
	}
	
	/// <summary>
	/// Update is called once per frame. ish
	/// </summary>
	void Update () {
		if (this._isFireing) {
			this._transform.position = new Vector2 (this._transform.position.x+0.05f, this._startPosition.y);
		}
	}


	/// <summary>
	/// Reset this instance.
	/// </summary>
	public void Reset(){
		this.transform.position = new Vector2 (this._playerTransform.position.x + 0.12f, this._playerTransform.position.y + 0.06200004f);
		this._isFireing = false;

		Debug.Log (this.transform.position);
	}

	/// <summary>
	/// Fire this instance.
	/// </summary>
	public void Fire(){
		this._isFireing = true;
	}

	/// <summary>
	/// Raises the collision enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnCollisionEnter2D(Collision2D other){
		Debug.Log ("HIT "+other.gameObject.tag);
		this.Reset ();
	}
}
