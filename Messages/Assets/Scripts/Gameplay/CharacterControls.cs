using UnityEngine;
using System.Collections;

public class CharacterControls : Photon.MonoBehaviour
{
    [Header("Camera management")]
    [SerializeField]
    private Transform m_CameraTargetsRoot;    

    [Header("Movement")]
    [SerializeField]
    private float m_Speed = 8f;

    [Header("Inventory")]
    [SerializeField] 
	private Inventory m_Inventory;

    private Vector3 m_MovementDirection;
    private Vector3 m_LastMovementDirection;

    public CameraManagement Camera { get; set; }

    protected void Start()
    {
        // Init camera
        for (int i = 0; i < m_CameraTargetsRoot.childCount ; i++)
        {
            print(Camera);
            print(Camera.CameraTargets);
            print(m_CameraTargetsRoot);
            Camera.CameraTargets[i] = m_CameraTargetsRoot.GetChild(i);
        }

        Camera.gameObject.SetActive(true);
    }

	protected void Update() 
	{
		if (photonView.isMine)
		{
            UpdateCameraTarget();

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

    private void UpdateCameraTarget()
    {
        Camera.UpdateCameraPosition(m_LastMovementDirection);
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