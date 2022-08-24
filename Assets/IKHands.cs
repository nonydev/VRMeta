using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace UnityChan
{
	[RequireComponent(typeof(Animator))]
	public class IKHands : MonoBehaviour, IPunObservable
	{

		private Animator anim;
		public Transform lookPos = null;
		public Transform rightHand = null;
		public Transform leftHand = null;
		public bool isIkActive = false;
		public float mixWeight = 1.0f;

		private Vector3 lookPosAlt, rightHandAlt, leftHandAlt;
		private Quaternion rightHandRot, leftHandRot;

		void Awake()
		{
			anim = GetComponent<Animator>();
		}

		void Update()
		{
			//Kobayashi
			if (mixWeight >= 1.0f)
				mixWeight = 1.0f;
			else if (mixWeight <= 0.0f)
				mixWeight = 0.0f;
		}

		void OnAnimatorIK(int layerIndex)
		{
			if (isIkActive)
			{
				if (lookPos != null)
				{
					lookPosAlt = lookPos.position;
				}

				if (rightHand != null)
				{
					rightHandAlt = rightHand.position;
					rightHandRot = rightHand.rotation;
				}

				if (leftHand != null)
				{
					leftHandAlt = leftHand.position;
					leftHandRot = leftHand.rotation;
				}
				anim.SetLookAtWeight(1);
				anim.SetLookAtPosition(lookPosAlt);
				anim.SetIKPositionWeight(AvatarIKGoal.RightHand, mixWeight);
				anim.SetIKRotationWeight(AvatarIKGoal.RightHand, mixWeight);
				anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandAlt);
				anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandRot);

				anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, mixWeight);
				anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, mixWeight);
				anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandAlt);
				anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRot);
			}
		}

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (stream.IsWriting)
			{
				stream.SendNext(lookPosAlt);
				stream.SendNext(rightHandAlt);
				stream.SendNext(rightHandRot);
				stream.SendNext(leftHandAlt);
				stream.SendNext(leftHandRot);
			}

			if (stream.IsReading)
			{
				lookPosAlt = (Vector3)stream.ReceiveNext();
				rightHandAlt = (Vector3)stream.ReceiveNext();
				rightHandRot = (Quaternion)stream.ReceiveNext();
				leftHandAlt = (Vector3)stream.ReceiveNext();
				leftHandRot = (Quaternion)stream.ReceiveNext();
			}
		}
	}
}