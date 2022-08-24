using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Base
{
	private PhotonView view;
	private Transform playerTransform;

	private bool interacting = false;
	private Rigidbody rb;

	private void Awake()
	{
		view = GetComponent<PhotonView>();
		rb = GetComponent<Rigidbody>();
		CacheMethod("Interact", (o) =>
		{
			OnInteraction();
		});
	}

	private void Update()
	{
		if (!view.IsMine)
		{
			rb.isKinematic = true;
		}

		if (!view.IsMine || playerTransform == null)
		{
			return;
		}

		var vec = Vector3.ProjectOnPlane(playerTransform.position, Vector3.up);
		transform.position = vec + playerTransform.forward * 1f + playerTransform.up * 0.8f;
	}

	public void OnInteraction()
	{
		if (interacting)
		{
			interacting = false;
			playerTransform = null;
			rb.isKinematic = false;
			return;
		}

		rb.isKinematic = true;
		view.RequestOwnership();
		interacting = true;
		var gos = GameObject.FindGameObjectsWithTag("Player");
		foreach (var go in gos)
		{
			var gov = go.GetComponent<PhotonView>();
			if (gov.IsMine)
			{
				var player = gov;
				playerTransform = player.transform;
			}
		}
	}
}
