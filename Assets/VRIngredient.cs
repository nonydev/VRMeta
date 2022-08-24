using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRIngredient : MonoBehaviour
{
	private Rigidbody rb;
	private PhotonView view;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		view = GetComponent<PhotonView>();
	}

	void Update()
	{
		if (view.IsMine == false)
		{
			rb.isKinematic = true;
			return;
		}

		rb.isKinematic = false;
	}
}
