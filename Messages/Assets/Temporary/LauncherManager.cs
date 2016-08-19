using UnityEngine;

public class LauncherManager : Photon.PunBehaviour
{
    [Header("General properties")]
    [SerializeField]
    private string m_GameVersion = "0.1";
    [SerializeField]
    private byte m_MaximumAmountOfPlayers = 1;

    [Header("Player Network UI")]
    [SerializeField]
    private Transform m_Menu;
    [SerializeField]
    private Transform m_Connecting;

    [Header("Debug purposes")]
    [SerializeField]
    private PhotonLogLevel m_PhotonLogLevel = PhotonLogLevel.Full;

    private bool m_IsConnecting;

    protected void Awake()
    {
        // TO DO: Remove when debugging is over.
        PhotonNetwork.logLevel = m_PhotonLogLevel;

        // Do not auto-join lobby
        PhotonNetwork.autoJoinLobby = false;

        // New players have the same "game" as the master client
        PhotonNetwork.automaticallySyncScene = true;
    }

    protected void Start()
    {
        UpdateUI(true, false);
    }

    public void Connect()
    {
        m_IsConnecting = true;
        UpdateUI(false, true);

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

    private void UpdateUI(bool a_Argument0, bool a_Argument1)
    {
        m_Menu.gameObject.SetActive(a_Argument0);
        m_Connecting.gameObject.SetActive(a_Argument1);
    }

    #region Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");

        if (m_IsConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            UpdateUI(false, false);
        }
    }

    // Used to tell the player there is no internet
    public override void OnDisconnectedFromPhoton()
    {
        Debug.Log("DisconnectedFromPhoton");
        UpdateUI(true, false);
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("CouldNotJoinRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = m_MaximumAmountOfPlayers }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("RoomJoined");

        if (PhotonNetwork.room.playerCount == 1)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("ImpossibleToJoinRoom");
    }
    #endregion
}