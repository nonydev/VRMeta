using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ScaleSphere : BasicTriggerInteraction
{
	public BasicGrab Target;
	public MeshRenderer Vis;

	public float XScale = 0.3f;

	private float originalDistance;
	private Vector3 originalScale;
	private Hand grabbingHand;
	private bool isGrabbed;

	private void Update()
	{
		Vis.enabled = Target.IsGrabbed;
		if(isGrabbed && !Target.IsGrabbed)
		{
			grabbingHand.DetachObject(gameObject);
			grabbingHand.HoverUnlock(interactable);
			isGrabbed = false;
		}

		if(!isGrabbed) {
			transform.position = Target.transform.position + XScale * (Target.transform.right * Target.transform.localScale.x);
		}

		if(isGrabbed)
		{
			float currentDistance = Vector3.Distance(Target.transform.position, transform.position);
			float rate = currentDistance / originalDistance;
			Target.transform.localScale = originalScale * rate;
		}
	}

	protected override void OnGrabStart(Hand hand, GrabTypes startingGrabType)
	{
		base.OnGrabStart(hand, startingGrabType);
		if(Target.IsGrabbed == false) {
			return;
		}
		isGrabbed = true;
		grabbingHand = hand;
		// Call this to continue receiving HandHoverUpdate messages,
		// and prevent the hand from hovering over anything else
		hand.HoverLock(interactable);

		// Attach this object to the hand
		hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

		originalDistance = Vector3.Distance(Target.transform.position, transform.position);
		originalScale = Target.transform.localScale;
	}

	protected override void OnGrabEnd(Hand hand, GrabTypes startingGrabType)
	{
		base.OnGrabEnd(hand, startingGrabType);
		isGrabbed = false;

		// Detach this object from the hand
		hand.DetachObject(gameObject);

		// Call this to undo HoverLock
		hand.HoverUnlock(interactable);
	}
}
