﻿using UnityEngine;
using System.Collections;

public class EndLevel_4 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
			Application.LoadLevel("Intermission_4");
	}
}
