using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BasicGrab : BasicTriggerInteraction
{
	public bool SnapToOld = false;
	public bool IsGrabbed
	{
		get {
			return isGrabbed;
		}
		private set {
			isGrabbed = value;
		}
	}
	private bool isGrabbed;

	private Vector3 oldPosition;
	private Quaternion oldRotation;
	protected override void OnGrabStart(Hand hand, GrabTypes startingGrabType)
	{
		base.OnGrabStart(hand, startingGrabType);
		IsGrabbed = true;
		// Save our position/rotation so that we can restore it when we detach
		oldPosition = transform.position;
		oldRotation = transform.rotation;

		// Call this to continue receiving HandHoverUpdate messages,
		// and prevent the hand from hovering over anything else
		hand.HoverLock(interactable);

		// Attach this object to the hand
		hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
	}

	protected override void OnGrabEnd(Hand hand, GrabTypes startingGrabType)
	{
		base.OnGrabEnd(hand, startingGrabType);
		IsGrabbed = false;

		// Detach this object from the hand
		hand.DetachObject(gameObject);

		// Call this to undo HoverLock
		hand.HoverUnlock(interactable);

		if (SnapToOld)
		{
			// Restore position/rotation
			transform.position = oldPosition;
			transform.rotation = oldRotation;
		}
	}
}
