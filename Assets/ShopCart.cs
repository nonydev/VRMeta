using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCart : Base
{
	private PhotonView view;
	private Transform playerTransform;
	private Basket basket;

	private bool interacting = false;

	private void Awake()
	{
		view = GetComponent<PhotonView>();
		basket = GetComponentInChildren<Basket>();
		CacheMethod("Interact", (o) =>
		{
			OnInteraction();
		});
	}

	public void OnInteraction()
	{
		if (interacting)
		{
			interacting = false;
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

		var vec = Vector3.ProjectOnPlane(playerTransform.position, Vector3.up);
		transform.position = vec + playerTransform.forward * 1.3f;
		transform.LookAt(vec + playerTransform.forward * 2);
	}
}
