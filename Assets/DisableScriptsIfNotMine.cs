using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class DisableScriptsIfNotMine : MonoBehaviour
{
	private PhotonView view;
	private HashSet<MonoBehaviour> Scripts;

	private bool oe = false;

	private IEnumerator Start()
	{
		yield return new WaitUntil(() =>
		{
			return PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom;
		});

		view = GetComponent<PhotonView>();
		var tv = GetComponent<PhotonTransformView>();

		MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
		Scripts = new HashSet<MonoBehaviour>(scripts);
		Scripts.Remove(view);
		Scripts.Remove(tv);
		Scripts.Remove(this);

		Refresh();
	}

	private void Refresh()
	{
		bool e = view.IsMine;
		oe = e;

		if (!e)
		{
			foreach (var s in Scripts)
			{
				Destroy(s);
			}

			foreach (var s in Scripts)
			{
				Destroy(s);
			}
		}

		Rigidbody rb = GetComponent<Rigidbody>();

		if (rb && !e)
		{
			Destroy(rb);
		}
		if (!e)
		{
			foreach (var c in GetComponents<Collider>())
			{
				Destroy(c);
			}
		}
	}
}
