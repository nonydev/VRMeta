using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRShopCart : Base
{

	private PhotonView view;
	private Transform handTransform;
	private Transform playerTransform;
	private Basket basket;

	private bool interacting = false;

	private void Awake()
	{
		view = GetComponent<PhotonView>();
		basket = GetComponentInChildren<Basket>();
		CacheMethod("Interact", (o) =>
		{
			handTransform = o as Transform;
			OnInteraction();
		});
	}

	public void OnInteraction()
	{
		if (interacting)
		{
			interacting = false;
			handTransform = null;
			playerTransform = null;
			basket.Unlock();
			return;
		}

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
		basket.Lock();
		foreach (var v in basket.GetComponentsInChildren<PhotonView>())
		{
			v.RequestOwnership();
		}
	}

	private void Update()
	{
		if (!view.IsMine || playerTransform == null)
		{
			return;
		}

		var forward = Vector3.ProjectOnPlane(handTransform.forward, Vector3.up);
		transform.position = Vector3.ProjectOnPlane(handTransform.position, Vector3.up) + forward * 1f;
		transform.LookAt(transform.position + forward);
	}
}
