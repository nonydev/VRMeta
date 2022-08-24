using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAvatar : MonoBehaviour
{
	public Transform Head;
	public Animator Target;

	void Update()
	{
		if (Head == null)
		{
			return;
		}

		float rate = Mathf.Clamp01(Head.position.y - 0.592f);
		Target.SetFloat("Head", rate);
	}
}
