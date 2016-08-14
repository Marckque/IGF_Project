using UnityEngine;
using System.Collections;

public class InitialiseGame : MonoBehaviour 
{
    [Header("Debug")]
    [SerializeField]
    private bool m_IsActivated;
    [Header("Prefabs")]
	[SerializeField]
	private GameObject m_Character;
    [SerializeField]
    private GameObject m_Camera;
    [Header("Game related")]
    [SerializeField]
    private Transform[] m_SpawnPositions;

    private string m_PrefabsFolder = "Prefabs/";

	public void Awake()
	{
        if (m_IsActivated)
        {
            InitialiseCamera();
        }
	}

    private void InitialiseCharacter(CameraManagement a_Camera)
	{
        int currentNumberOfPlayers = PhotonNetwork.playerList.Length;

		GameObject character = PhotonNetwork.Instantiate(m_PrefabsFolder + m_Character.gameObject.name, m_SpawnPositions[currentNumberOfPlayers-1].position, Quaternion.identity, 0);
        character.GetComponent<CharacterControls>().Camera = a_Camera;
	}

    public void InitialiseCamera()
    {
        GameObject camera = PhotonNetwork.Instantiate(m_PrefabsFolder + m_Camera.gameObject.name, Vector3.zero, m_Camera.transform.rotation, 0);
        CameraManagement cameraManagement = camera.GetComponentInChildren<CameraManagement>();

        InitialiseCharacter(cameraManagement);
    }
}