using UnityEngine;
using System.Collections;

public class CameraManagement : MonoBehaviour
{
    [SerializeField]
    private float m_LerpSpeed = 0.75f;

    private Vector3 m_CurrentTargetPosition;

    public Transform[] CameraTargets { get; set; }

    private void Awake()
    {
        CameraTargets = new Transform[2];
    }

    public void UpdateCameraPosition(Vector3 a_LastDirection)
    {
        if (CameraTargets.Length > 2)
        {
            throw new System.Exception("There are more than 2 possible camera targets");
        }

        m_CurrentTargetPosition = (a_LastDirection.z >= 0) ? CameraTargets[0].position : CameraTargets[1].position; 

        Vector3 targetPosition = new Vector3(m_CurrentTargetPosition.x, m_CurrentTargetPosition.y, m_CurrentTargetPosition.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, m_LerpSpeed * Time.deltaTime);
    }

    // Debug purposes
    protected void OnDrawGizmos()
    { 
        Gizmos.DrawWireSphere(m_CurrentTargetPosition, 0.25f);
    }
}