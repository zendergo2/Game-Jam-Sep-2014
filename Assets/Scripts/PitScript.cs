using UnityEngine;
using System.Collections;

public class PitScript : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
			Application.LoadLevel(Application.loadedLevel);
	}
}
