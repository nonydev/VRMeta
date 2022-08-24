
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;
using UnityEngine.AI;
public class VRPlayerEntity : MonoBehaviour
{
	public GameObject Owner;

	public Transform CameraSlot;
	public Transform BasketSlot;

	public Transform rightHand, leftHand, lookPos, head, player;

	private PhotonView entity;
	private NavMeshAgent agent;

	public float Speed = 5;
	public float flatSizeIncrement = 0.1f;

	public float AbsorbRate = 0.1f;

	public float SpawnRadius = 15;

	private const string PlayerPrefabName = "Player";

	private void Awake()
	{
		entity = GetComponent<PhotonView>();
		agent = GetComponent<NavMeshAgent>();
		if (entity.AmOwner)
		{
			Respawn();
			//var go = PhotonNetwork.Instantiate("Basket", BasketSlot.transform.position, BasketSlot.rotation);
			//go.transform.parent = BasketSlot;

			SpawnAvatar();
		}
		Owner.SetActive(entity.AmOwner);

		name = PlayerPrefabName;
	}

	private void SpawnAvatar()
	{
		var go = PhotonNetwork.Instantiate("Avatar", transform.position, transform.rotation);
		go.transform.parent = player;
		var ik = go.GetComponent<IKHands>();
		ik.rightHand = rightHand;
		ik.leftHand = leftHand;
		ik.lookPos = lookPos;

		var avatar = go.GetComponent<VRAvatar>();
		avatar.Head = head;
	}

	private void Update()
	{
		if (entity.AmOwner == false)
		{
			return;
		}

	}


	private void OnTriggerEnter(Collider other)
	{
		if (!entity.AmOwner)
		{
			return;
		}
	}


	private void Respawn()
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
	}
}
