using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private Vector2 _startPosition;
	private Transform _transform;
	private Transform _playerTransform;
	private bool _isFireing;


	//
	public GameObject Player;

	// Use this for initialization
	void Start () {
		this._playerTransform = Player.gameObject.GetComponent<Transform> ();
		this._startPosition = new Vector2 ((this._playerTransform.position.x) + 0.12f, this._playerTransform.position.y + 0.06200004f);
		this._transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this._isFireing) {
			this._transform.position = new Vector2 (this._transform.position.x+0.05f, this._startPosition.y);
		}
	}

	public void Reset(){
		this.transform.position = new Vector2 (this._playerTransform.position.x + 0.12f, this._playerTransform.position.y + 0.06200004f);
		this._isFireing = false;

		Debug.Log (this.transform.position);
	}

	public void Fire(){
		this._isFireing = true;
	}


	private void OnCollisionEnter2D(Collision2D other){
		Debug.Log ("HIT "+other.gameObject.tag);
		this.Reset ();
	}
}
