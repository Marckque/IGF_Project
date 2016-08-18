using UnityEngine;

namespace ProjectIGF_Tameus_Marckque
{
    public class OnlyActiveForThisPlayer : Photon.MonoBehaviour
    {
        protected void Awake()
        {
            if (!photonView.isMine)
            {
                gameObject.SetActive(false);
            }
        }
    }
}