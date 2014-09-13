using UnityEngine;
using System.Collections;

public class NPCJumper : MonoBehaviour
{
	private bool isTranslucent = true;


	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && !isTranslucent)
		{
			isTranslucent = true;

		}
		else if (Input.GetKeyDown (KeyCode.Space) && isTranslucent)
		{
			isTranslucent = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log("Hello");
		if (coll.collider != null && !isTranslucent)
			coll.gameObject.SendMessage("increaseJump", 5f);
	}
}
