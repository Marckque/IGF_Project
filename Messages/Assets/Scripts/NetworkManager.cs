using UnityEngine;
using System.Collections;
using Photon;

public class NetworkManager : Photon.MonoBehaviour
{
	[SerializeField]
	private string m_CurrentGameVersion;
	[SerializeField]
	private InitialiseGame m_InitialiseGame;

	protected void Start()
	{
		PhotonNetwork.ConnectUsingSettings(m_CurrentGameVersion);
	}

	void OnConnectedToMaster()
	{
		Debug.Log("Connected to master!");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnJoinLobby()
	{
		Debug.Log("Lobby joined!");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Cannot join random room!");
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
	}

	void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.Log("Cause :" + cause);
	}

	void OnCreatedRoom()
	{
		Debug.Log("Room has been created!");
	}

	void OnJoinedRoom()
	{
		// Same function in InitialiseGame ? 
		Debug.Log("Room has been joined!");
	}
}