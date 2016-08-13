using UnityEngine;
using System.Collections;

public class CharacterControls : Photon.MonoBehaviour
{
    [Header("Camera management")]
    [SerializeField]
    private CameraTarget m_CameraTarget;
    [SerializeField]
    private float m_CameraTargetOffset = 2f;

    [Header("Movement")]
    [SerializeField]
    private float m_Speed = 8f;

    [Header("Inventory")]
    [SerializeField] 
	private Inventory m_Inventory;


    private Vector3 m_MovementDirection;
    private Vector3 m_LastMovementDirection;

	protected void Update () 
	{
		if (photonView.isMine)
		{
            UpdateCamera();

			if (!m_Inventory.IsWriting)
			{
                Movement();
			}

			if (Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				if (!m_Inventory.gameObject.activeInHierarchy)
				{
					ActivateInventory();
				}
				else
				{
					DeactivateInventory();
				}
			}
		}
	}

    private void Movement()
    {
        m_MovementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        m_MovementDirection.Normalize();

        if (m_MovementDirection != Vector3.zero)
        {
            m_LastMovementDirection = m_MovementDirection;
            transform.Translate(m_MovementDirection * Time.deltaTime * m_Speed);
        }
    }

    private void UpdateCamera()
    {
        //m_CameraTarget.transform.position = transform.position + m_LastMovementDirection.normalized * m_CameraTargetOffset;
    }

    private void ActivateInventory()
	{
		m_Inventory.gameObject.SetActive(true);
    }

	private void DeactivateInventory()
	{
		m_Inventory.gameObject.SetActive(false);
	}
}