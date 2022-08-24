using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveTargetWithMine : MonoBehaviour
{
	public List<GameObject> Targets;
	private PhotonView view;
	private bool inverse = true;
	private void Awake()
	{
		view = GetComponent<PhotonView>();
	}

	private void Update()
	{
		foreach (var target in Targets)
			target.SetActive(inverse ? !view.IsMine : view.IsMine);
	}
}
