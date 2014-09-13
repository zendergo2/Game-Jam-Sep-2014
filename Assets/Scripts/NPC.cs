﻿using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{

	protected Animator animator;
	
	void Start()
	{
		animator = GetComponent<Animator>();
		gameObject.layer = 8;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space) && gameObject.layer == 8)
		{
			gameObject.layer = 0;
			animator.SetBool("Translucent", false);
		}
		else if (Input.GetKeyDown (KeyCode.Space) && gameObject.layer == 0)
		{
			gameObject.layer = 8;
			animator.SetBool("Translucent", true);
		}
	}
}
