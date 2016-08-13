using UnityEngine;
using System.Collections;

public class InitialiseGame : MonoBehaviour 
{
	[SerializeField]
	private Character m_Character;

	private string m_PrefabsFolder = "Prefabs/";

	public void Initialise()
	{
		InitialiseCharacter();
	}

	private void InitialiseCharacter()
	{
        print("Spawn char");
		PhotonNetwork.Instantiate(m_PrefabsFolder + m_Character.gameObject.name, Vector3.zero, Quaternion.identity, 0);
	}
}