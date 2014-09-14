using UnityEngine;
using System.Collections;

public class NPCJumpBlock : MonoBehaviour {
	
	void Start()
	{
		gameObject.tag = "Untagged";
	}
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (gameObject.tag == "Untagged")
				gameObject.tag = "JumpBlock";
			else if (gameObject.tag == "JumpBlock")
				gameObject.tag = "Untagged";
		}
	}
}
