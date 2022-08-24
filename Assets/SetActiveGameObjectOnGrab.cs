using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SetActiveGameObjectOnGrab : BasicTriggerInteraction
{
    public GameObject Target;
	protected override void OnGrabStart(Hand hand, GrabTypes startingGrabType)
	{
		base.OnGrabStart(hand, startingGrabType);
		Target.SetActive(true);
	}

	protected override void OnGrabEnd(Hand hand, GrabTypes startingGrabType)
	{
		base.OnGrabEnd(hand, startingGrabType);
		Target.SetActive(false);
	}
}
