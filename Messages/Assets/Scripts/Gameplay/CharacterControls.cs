using UnityEngine;

public class CharacterControls : Photon.MonoBehaviour
{   
    [Header("Movement")]
    [SerializeField]
    private float m_Speed = 8f;

    private Vector3 m_MovementDirection;
    private Vector3 m_LastMovementDirection;

    public CameraManagement Camera { get; set; }

    protected void Update()
    {
        if (!photonView.isMine)
        {
            return;
        }

        Movement();
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
}

/*using UnityEngine;
using System.Collections;

public class CharacterControls : Photon.MonoBehaviour
{
    [Header("Camera management")]
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private Transform m_TargetsPositionsRoot;

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
        //InitialiseCamera();
    }

	protected void Update() 
	{
        if (!photonView.isMine)
        {
            return;
        }

        //UpdateCamera();

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

    private void InitialiseCamera()
    {
        for (int i = 0; i < m_TargetsPositionsRoot.childCount; i++)
        {
            Camera.TargetsPositions[i] = m_TargetsPositionsRoot.GetChild(i);
        }

        Camera.CurrentTarget = m_Target;
        Camera.gameObject.SetActive(true);
    }

    private void UpdateCamera()
    {
        Camera.UpdatePosition(m_LastMovementDirection);
        Camera.UpdateAngle(m_LastMovementDirection);
    }

    private void ActivateInventory()
	{
		m_Inventory.gameObject.SetActive(true);
    }

	private void DeactivateInventory()
	{
		m_Inventory.gameObject.SetActive(false);
	}
}*/
