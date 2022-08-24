using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPlayer : MonoBehaviour
{
	public Transform dest;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.transform.root.position = dest.position;
			// other.transform.position = dest.position;
		}
	}
}
