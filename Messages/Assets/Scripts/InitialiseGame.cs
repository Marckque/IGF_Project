using UnityEngine;
using System.Collections;

public class InitialiseGame : MonoBehaviour 
{
	[SerializeField]
	private GameObject m_Character;
    [SerializeField]
    private Transform[] m_SpawnPositions;
    [SerializeField]
    private GameObject m_Camera;

	private string m_PrefabsFolder = "Prefabs/";

	public void Initialise()
	{
        InitialiseCamera();
	}

    private void InitialiseCharacter(CameraManagement a_Camera)
	{
        int currentNumberOfPlayers = PhotonNetwork.playerList.Length;

		GameObject character = PhotonNetwork.Instantiate(m_PrefabsFolder + m_Character.gameObject.name, m_SpawnPositions[currentNumberOfPlayers-1].position, Quaternion.identity, 0);
        character.GetComponent<CharacterControls>().Camera = a_Camera;
	}

    private void InitialiseCamera()
    {
        GameObject camera = PhotonNetwork.Instantiate(m_PrefabsFolder + m_Camera.gameObject.name, Vector3.zero, m_Camera.transform.rotation, 0);
        CameraManagement cameraManagement = camera.GetComponentInChildren<CameraManagement>();

        InitialiseCharacter(cameraManagement);
    }
}