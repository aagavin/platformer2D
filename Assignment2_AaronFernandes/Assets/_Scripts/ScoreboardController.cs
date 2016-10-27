using UnityEngine;
using System.Collections;
using System;


/*
 * Aaron Fernandes
 * 300773526
 * 
 * 2D Platformer
 * 
 */ 
/// <summary>
/// Scoreboard controller.
/// </summary>

using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour {

	//PRIVATE INSTANCE VARABLES
	private float _score;
	private float _time;

	//PUBLIC INSTAND VARABLES
	public Text TimeText;
	public Text ScoreText;

	/// <summary>
	/// Gets or sets the score.
	/// </summary>
	/// <value>The score.</value>
	public float Score {
		get{
			return this._score;
		}
		set{
			this._score = value;
			this.ScoreText.text = "Score: " + this._score;
		}
	}

	public float Time {
		get{
			return this._time;
		}
		set{
			this._time = value;
		}
	}



	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
		this._time = 500f;
	}
	
	/// <summary>
	/// Updates the remaining time for the player
	/// </summary>
	void Update () {
		this._time -= UnityEngine.Time.deltaTime;
		TimeText.text = "Time Left: " + Mathf.Round(this._time * 100f) /100f;
	}
}
