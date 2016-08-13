using Photon;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugUI : Photon.MonoBehaviour
{
    [SerializeField]
    private Text m_MasterClientText;
	
	protected void Update ()
    {
        m_MasterClientText.text = "Is master client: " + PhotonNetwork.isMasterClient;
	}
}
