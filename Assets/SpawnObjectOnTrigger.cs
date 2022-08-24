using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpawnObjectOnTrigger : BasicTriggerInteraction
{
	public string ObjectName;
	protected override void OnGrabStart(Hand hand, GrabTypes startingGrabType)
	{
		base.OnGrabStart(hand, startingGrabType);
		
		GameObject go = PhotonNetwork.Instantiate(ObjectName, hand.objectAttachmentPoint.position, Quaternion.identity);
		Interactable first = go.GetComponentInChildren<Interactable>();
		hand.HoverLock(first);
		hand.AttachObject(first.gameObject, startingGrabType, attachmentFlags);
	}
}
