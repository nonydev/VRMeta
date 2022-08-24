using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandInteraction : Base
{
	private Hand hand;
	private GameObject target;

	// Start is called before the first frame update
	void Awake()
	{
		hand = GetComponent<Hand>();
		hand.uiInteractAction.AddOnStateDownListener(TriggerDown, hand.handType);
		hand.uiInteractAction.AddOnStateUpListener(TriggerUp, hand.handType);
	}
	public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		Call("Interact", target, transform);
		target = null;
	}

	public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		var cs = Physics.OverlapBox(transform.position, Vector3.one / 5, Quaternion.identity, LayerMask.GetMask("Interactible"), QueryTriggerInteraction.Ignore);
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
			Call("Interact", target, transform);
		}
	}
}
