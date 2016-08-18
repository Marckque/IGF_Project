using UnityEngine;

public class InitialiseGame : Photon.PunBehaviour 
{
    [Header("Prefabs")]
	[SerializeField]
	private GameObject m_Character;
    [SerializeField]
    private GameObject m_Camera;
    [Header("Game related")]
    [SerializeField]
    private Transform[] m_SpawnPositions;

    private string m_PrefabsFolder = "Prefabs/";

    static public InitialiseGame Instance;
    
    public void Awake()
    {
        Instance = this;
    }

    public void InitialisePlayer()
	{
        if (PhotonNetwork.inRoom)
        {
            CreatePlayer();
        }
	}

    public void CreatePlayer()
    {
        int currentNumberOfPlayers = PhotonNetwork.playerList.Length;

        GameObject character = PhotonNetwork.Instantiate(m_PrefabsFolder + m_Character.gameObject.name, m_SpawnPositions[currentNumberOfPlayers - 1].position, Quaternion.identity, 0);

        string newName = (currentNumberOfPlayers == 1) ? "Master" : "Client";
        character.name = "Character_" + newName;
    }

    /*
    public override void OnJoinedRoom()
    {
        print("New player in the level !");
        InitialiseCamera();
    }    
    */

        /*
    private void InitialiseCharacter(CameraManagement a_Camera)
	{
        int currentNumberOfPlayers = PhotonNetwork.playerList.Length;

		GameObject character = PhotonNetwork.Instantiate(m_PrefabsFolder + m_Character.gameObject.name, m_SpawnPositions[currentNumberOfPlayers-1].position, Quaternion.identity, 0);

        string newName = (currentNumberOfPlayers == 1) ? "Master" : "Client";
        character.name = "Character_" + newName;
        character.GetComponent<CharacterControls>().Camera = a_Camera;
        a_Camera.LinkedPlayer = character.gameObject.name;
	}

    public void InitialiseCamera()
    {
        print("Getting triggered");

        GameObject camera = PhotonNetwork.Instantiate(m_PrefabsFolder + m_Camera.gameObject.name, Vector3.zero, m_Camera.transform.rotation, 0);
        CameraManagement cameraManagement = camera.GetComponentInChildren<CameraManagement>();

        InitialiseCharacter(cameraManagement);
    }
    */
}