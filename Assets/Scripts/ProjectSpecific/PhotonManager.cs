using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
	public bool AutoJoin = true;
	private bool connected;
	public bool Connected { get => connected; set => connected = value; }

	private bool joining;
	public bool Joining { get => joining; set => joining = value; }

	public string PlayerType = "PlayerSphere";

	private const string RoomName = "Temp";

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		base.OnConnectedToMaster();
		Debug.Log("Connected");
		Connected = true;
	}

	private void Update()
	{
		if (Connected && !Joining && (Input.GetKeyDown(KeyCode.J) || AutoJoin))
		{
			Joining = true;
			RoomOptions option = new RoomOptions();
			option.IsOpen = true;
			option.IsVisible = true;
			option.MaxPlayers = 100;
			bool success = PhotonNetwork.JoinOrCreateRoom(RoomName, option, TypedLobby.Default);
			Debug.Log("Joining: " + success);
		}
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		base.OnJoinRandomFailed(returnCode, message);
		Debug.Log(message);
	}

	public override void OnCreatedRoom()
	{
		base.OnCreatedRoom();
		Debug.Log("room created");
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		base.OnCreateRoomFailed(returnCode, message);
		Debug.Log(message);
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		base.OnPlayerEnteredRoom(newPlayer);
		Debug.Log(newPlayer.UserId + " joined");
	}

	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		Debug.Log("room joined");
		SpawnPlayer();
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		base.OnJoinRoomFailed(returnCode, message);
		Debug.Log(message);
	}

	private void SpawnPlayer()
	{
		GameObject go = PhotonNetwork.Instantiate(PlayerType, Vector3.zero, Quaternion.Euler(0, 180, 0));
		if (TPArea != null)
		{
			TPArea.SetActive(true);
		}
	}
	public GameObject TPArea;
}
