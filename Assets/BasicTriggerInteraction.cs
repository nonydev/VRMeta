using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;

//-------------------------------------------------------------------------
[RequireComponent(typeof(Interactable))]
public class BasicTriggerInteraction : MonoBehaviour
{


	protected Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

	protected Interactable interactable;

	//-------------------------------------------------
	protected virtual void Awake()
	{
		interactable = this.GetComponent<Interactable>();
	}


	//-------------------------------------------------
	// Called when a Hand starts hovering over this object
	//-------------------------------------------------
	protected virtual void OnHandHoverBegin(Hand hand)
	{
		//
	}


	//-------------------------------------------------
	// Called when a Hand stops hovering over this object
	//-------------------------------------------------
	protected virtual void OnHandHoverEnd(Hand hand)
	{
		//
	}


	//-------------------------------------------------
	// Called every Update() while a Hand is hovering over this object
	//-------------------------------------------------
	protected virtual void HandHoverUpdate(Hand hand)
	{
		GrabTypes startingGrabType = hand.GetGrabStarting();
		bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

		if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
		{
			OnGrabStart(hand, startingGrabType);
		}
		else if (isGrabEnding)
		{
			OnGrabEnd(hand, startingGrabType);
		}
	}

	protected virtual void OnGrabStart(Hand hand, GrabTypes startingGrabType)
	{
		//
	}

	protected virtual void OnGrabEnd(Hand hand, GrabTypes startingGrabType)
	{
		//
	}

	//-------------------------------------------------
	// Called when this GameObject becomes attached to the hand
	//-------------------------------------------------
	protected virtual void OnAttachedToHand(Hand hand)
	{
		//
	}



	//-------------------------------------------------
	// Called when this GameObject is detached from the hand
	//-------------------------------------------------
	protected virtual void OnDetachedFromHand(Hand hand)
	{
		//
	}


	//-------------------------------------------------
	// Called every Update() while this GameObject is attached to the hand
	//-------------------------------------------------
	protected virtual void HandAttachedUpdate(Hand hand)
	{
	}

	private bool lastHovering = false;
	private void Update()
	{
		if (interactable.isHovering != lastHovering) //save on the .tostrings a bit
		{
			lastHovering = interactable.isHovering;
		}
	}


	//-------------------------------------------------
	// Called when this attached GameObject becomes the primary attached object
	//-------------------------------------------------
	protected virtual void OnHandFocusAcquired(Hand hand)
	{
		//
	}


	//-------------------------------------------------
	// Called when another attached GameObject becomes the primary attached object
	//-------------------------------------------------
	protected virtual void OnHandFocusLost(Hand hand)
	{
		//
	}
}
