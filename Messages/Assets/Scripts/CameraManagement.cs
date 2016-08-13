using UnityEngine;
using System.Collections;

public class CameraManagement : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)]
    private float m_LerpSpeed = 0.75f;
    [SerializeField, Range(0.1f, 1f)]
    private float m_LerpSpeedMultiplier;

    private float m_OriginalLerpSpeed;

    public Transform[] TargetsPositions { get; set; }
    public Transform CurrentTarget { get; set; }
    
    private void Awake()
    {
        TargetsPositions = new Transform[2];
        m_OriginalLerpSpeed = m_LerpSpeed;
    }
    
    public void UpdatePosition(Vector3 a_LastDirection)
    {
        if (TargetsPositions.Length > 2)
        {
            throw new System.Exception("There are more than 2 possible camera targets positions");
        }

        UpdateLerpSpeed();

        CurrentTarget.position = TargetsPositions[1].position;

        // In case we need several camera target positions
        //CurrentTarget.position = (a_LastDirection.z > 0) ? TargetsPositions[1].position : TargetsPositions[1].position; 

        Vector3 targetPosition = new Vector3(CurrentTarget.position.x, CurrentTarget.position.y, CurrentTarget.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, m_LerpSpeed * Time.deltaTime);
    }

    public void UpdateAngle(Vector3 a_LastDirection)
    {
        // not used for now
    }

    private void UpdateLerpSpeed()
    {
        float distance = Vector3.Distance(CurrentTarget.position, transform.position);

        m_LerpSpeed = m_OriginalLerpSpeed * (distance * m_LerpSpeedMultiplier);
    }
}