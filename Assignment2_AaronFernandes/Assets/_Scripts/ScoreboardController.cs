using UnityEngine;
using System.Collections;
using System;


using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour {

	// 
	private float _score;
	private float _time;

	//
	public Text TimeText;
	public Text ScoreText;


	public float Score {
		get{
			return this._score;
		}
		set{
			this._score = value;
			this.ScoreText.text = "Score: " + this._score;
		}
	}

	// Use this for initialization
	void Start () {
		this._time = 500f;
	}
	
	// Update is called once per frame
	void Update () {
		this._time -= Time.deltaTime;
		TimeText.text = "Time Left: " + this._time;
	}
}
