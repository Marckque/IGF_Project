using UnityEngine;

public class CharacterControls : Photon.MonoBehaviour
{   
    [Header("Movement")]
    [SerializeField]
    private float m_Speed = 8f;

    [Header("Inventory")]
    [SerializeField]
    private Inventory m_Inventory;

    private Vector3 m_MovementDirection;
    //private Vector3 m_LastMovementDirection;

    protected void Update()
    {
        if (!photonView.isMine)
        {
            return;
        }

        Movement();
        Inventory();
    }

    private void Movement()
    {
        m_MovementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        m_MovementDirection.Normalize();

        if (m_MovementDirection != Vector3.zero)
        {
            //m_LastMovementDirection = m_MovementDirection;
            transform.Translate(m_MovementDirection * Time.deltaTime * m_Speed);
        }
    }

    private void Inventory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!m_Inventory.gameObject.activeInHierarchy)
            {
                m_Inventory.ActivateInventory();
            }
            else
            {
                m_Inventory.DeactivateInventory();
            }
        }
    }
}