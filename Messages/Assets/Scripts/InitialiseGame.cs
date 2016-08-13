using UnityEngine;
using System.Collections;

public class InitialiseGame : MonoBehaviour 
{
	[SerializeField]
	private GameObject m_Character;
    [SerializeField]
    private GameObject m_Camera;

	private string m_PrefabsFolder = "Prefabs/";

	public void Initialise()
	{
        InitialiseCamera();
	}

    private void InitialiseCharacter(CameraManagement a_Camera)
	{
		GameObject character = PhotonNetwork.Instantiate(m_PrefabsFolder + m_Character.gameObject.name, Vector3.zero, Quaternion.identity, 0);
        character.GetComponent<CharacterControls>().Camera = a_Camera;
	}

    private void InitialiseCamera()
    {
        GameObject camera = PhotonNetwork.Instantiate(m_PrefabsFolder + m_Camera.gameObject.name, Vector3.zero, m_Camera.transform.rotation, 0);
        CameraManagement cameraManagement = camera.GetComponentInChildren<CameraManagement>();

        InitialiseCharacter(cameraManagement);
    }
}