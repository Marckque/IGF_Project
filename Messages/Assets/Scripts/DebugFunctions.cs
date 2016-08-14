using UnityEngine;
using System.Collections;

public class DebugFunctions : MonoBehaviour
{
    [Header("Network related")]
    [SerializeField]
    private PhotonLogLevel m_PhotonLogLevel = PhotonLogLevel.Full;

    protected void Start()
    {
        PhotonNetwork.logLevel = m_PhotonLogLevel;
    }
}