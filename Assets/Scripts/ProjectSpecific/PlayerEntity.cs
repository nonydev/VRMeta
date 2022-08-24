using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerEntity : MonoBehaviour
{
	public GameObject Owner;

	public Transform CameraSlot;

	private PhotonView entity;
	private NavMeshAgent agent;

	public float Speed = 5;
	public float flatSizeIncrement = 0.1f;

	public float AbsorbRate = 0.1f;

	public float SpawnRadius = 15;

	public List<MonoBehaviour> OwnerScripts = new List<MonoBehaviour>();

	private const string PlayerPrefabName = "Player";

	private void Awake()
	{
		entity = GetComponent<PhotonView>();
		agent = GetComponent<NavMeshAgent>();
		if (entity.IsMine)
		{
			Respawn();
		}
		else
		{
			foreach (var script in OwnerScripts)
			{
				script.enabled = false;
			}
		}
		Owner.SetActive(entity.IsMine);

		name = PlayerPrefabName;
	}

	private void Update()
	{
		if (entity.IsMine == false)
		{
			return;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			agent.Move(Camera.main.transform.right * Time.deltaTime * Speed);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			agent.Move(-Camera.main.transform.right * Time.deltaTime * Speed);
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			agent.Move(-Camera.main.transform.forward * Time.deltaTime * Speed);
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			agent.Move(Camera.main.transform.forward * Time.deltaTime * Speed);
		}
	}


	//private void OnTriggerEnter(Collider other)
	//{
	//	if (!entity.AmOwner)
	//	{
	//		return;
	//	}
	//	if (other.transform.name == PlayerPrefabName)
	//	{
	//		if (transform.localScale.x > other.transform.localScale.x)
	//		{
	//			transform.localScale += other.transform.localScale * AbsorbRate;
	//		}
	//		else if (transform.localScale.x < other.transform.localScale.x)
	//		{
	//			Respawn();
	//		}
	//	}

	//	if (other.name == "FoodSphere")
	//	{
	//		PhotonView v = other.gameObject.GetComponent<PhotonView>();
	//		v.RPC("Suicide", RpcTarget.All);
	//		transform.localScale += Vector3.one * flatSizeIncrement;
	//	}
	//}


	private void Respawn()
	{
		transform.localScale = Vector3.one;
		Transform camera = GameObject.Find("Main Camera").transform; //Camera.main.transform;
		camera.SetParent(CameraSlot);
		camera.localPosition = new Vector3(0, 0, 0);
		camera.localRotation = Quaternion.identity;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.transform.name);
	}
}
