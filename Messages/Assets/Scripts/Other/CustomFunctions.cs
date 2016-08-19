using UnityEngine;
using System.Collections;

public class CustomFunctions : Photon.MonoBehaviour
{
    public static void DeactivateSelfOnSelfCheck(ref GameObject a_GameObject)
    {
        if (a_GameObject.GetPhotonView() != null)
        {
            if (a_GameObject.GetPhotonView().isMine)
            {
                a_GameObject.SetActive(true);
            }
            else
            {
                a_GameObject.SetActive(false);
            }
        }
    }

    public static void DeactivateSelfOnParentCheck(ref Transform a_Transform)
    {
        GameObject parent = a_Transform.transform.parent.gameObject;

        if (parent.GetPhotonView() != null)
        {
            if (parent.GetPhotonView().isMine)
            {
                a_Transform.gameObject.SetActive(true);
            }
            else
            {
                a_Transform.gameObject.SetActive(false);
            }
        }
    }

    public static void DeactivateSelfOnRootCheck(ref Transform a_Transform)
    {
        GameObject root = a_Transform.transform.parent.gameObject;

        if (root.GetPhotonView() != null)
        {
            if (root.GetPhotonView().isMine)
            {
                a_Transform.gameObject.SetActive(true);
            }
            else
            {
                a_Transform.gameObject.SetActive(false);
            }
        }
    }
}