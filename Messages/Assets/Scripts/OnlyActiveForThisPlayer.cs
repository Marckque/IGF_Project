using Photon;
using UnityEngine;

public class OnlyActiveForThisPlayer : Photon.MonoBehaviour
{
	protected void Start()
    {
        if (!photonView.isMine)
        {
            gameObject.SetActive(false);
        }
	}
}