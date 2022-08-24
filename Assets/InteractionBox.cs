using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBox : Base
{
	private PhotonView view;

	private bool IsInteracting = false;
	private GameObject target;

	private void Awake()
	{
		view = GetComponentInParent<PhotonView>();
	}

	void Update()
	{
		if (!view.IsMine)
		{
			return;
		}

		if (IsInteracting && Input.GetKeyDown(KeyCode.F))
		{
			Call("Interact", target);
			IsInteracting = false;
			target = null;
			return;
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			var cs = Physics.OverlapBox(transform.position, Vector3.one, Quaternion.identity, LayerMask.GetMask("Interactible"), QueryTriggerInteraction.Ignore);
			float minDist = float.MaxValue;
			Collider closest = null;
			foreach (var c in cs)
			{
				if (!c.CompareTag("Interactible"))
				{
					continue;
				}
				float dist = (Vector3.Distance(transform.position, c.transform.position));
				if (dist < minDist)
				{
					minDist = dist;
					closest = c;
				}
			}

			if (closest != null)
			{
				target = closest.transform.gameObject;
				IsInteracting = true;
				Call("Interact", target);
			}
		}
	}
}
