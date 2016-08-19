using UnityEngine;

public class CameraManagement : Photon.PunBehaviour
{
    [Header("Camera Target")]
    [SerializeField]
    private Transform m_CameraTarget;

    [Header("Camera Movement")]
    [SerializeField, Range(0.1f, 5f)]
    private float m_LerpSpeed;
    [SerializeField, Range(0.1f, 1f)]
    private float m_LerpSpeedMultiplier;

    private float m_OriginalLerpSpeed;

    protected void Awake()
    {
        Transform self = transform;
        CustomFunctions.DeactivateSelfOnRootCheck(ref self);

        m_OriginalLerpSpeed = m_LerpSpeed;
    }
    
    protected void Update()
    {
        UpdateLerpSpeed();
        UpdatePosition();
    }

    public void UpdatePosition()
    { 
        Vector3 targetPosition = new Vector3(m_CameraTarget.position.x, m_CameraTarget.position.y, m_CameraTarget.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, m_LerpSpeed * Time.deltaTime);
    }

    private void UpdateLerpSpeed()
    {
        float distance = Vector3.Distance(m_CameraTarget.position, transform.position);

        m_LerpSpeed = m_OriginalLerpSpeed * (distance * m_LerpSpeedMultiplier);
    }
}