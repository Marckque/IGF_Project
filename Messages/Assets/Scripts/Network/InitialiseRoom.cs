using UnityEngine;

public class InitialiseRoom : Photon.PunBehaviour
{
    [Header("General properties")]
    [SerializeField]
    private string m_GameVersion = "0.1";
    [SerializeField]
    private byte m_MaximumAmountOfPlayers = 2;

    [Header("Player properties")]
    [SerializeField]
    private GameObject m_Avatar;
    [SerializeField]
    private Transform[] m_SpawnPositions = new Transform[2];

    [Header("Debug purposes")]
    [SerializeField]
    private PhotonLogLevel m_PhotonLogLevel = PhotonLogLevel.Full;

    private bool m_IsConnecting;
    private bool m_HasSpawnedGameInitialise;

    protected void Awake()
    {
        // TO DO: Remove when debugging is over.
        PhotonNetwork.logLevel = m_PhotonLogLevel;

        // Do not auto-join lobby
        PhotonNetwork.autoJoinLobby = false;

        // New players have the same "game" as the master client
        PhotonNetwork.automaticallySyncScene = true;

        Connect();
    }

    public void Connect()
    {
        m_IsConnecting = true;        

        // If we are connected
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        // Else get the proper connection information
        else
        {
            PhotonNetwork.ConnectUsingSettings(m_GameVersion);
        }
    }

    private void InstantiatePlayer()
    {        
        int currentNumberOfPlayers = PhotonNetwork.playerList.Length;

        GameObject character = PhotonNetwork.Instantiate(m_Avatar.gameObject.name, m_SpawnPositions[currentNumberOfPlayers - 1].position, Quaternion.identity, 0);

        string newName = (currentNumberOfPlayers == 1) ? "Master" : "Client";
        character.name = "Character_" + newName;
    }

    #region Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        
        if (m_IsConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    // Used to tell the player there is no internet
    public override void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnDisconnectedFromPhoton");
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("OnPhotonRandomJoinFailed");

        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = m_MaximumAmountOfPlayers }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        InstantiatePlayer();
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("OnPhotonJoinRoomFailed");
    }
    #endregion
}