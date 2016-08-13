using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private float m_LerpSpeed = 0.75f;

    protected void Update()
    {
        Vector3 targetPosition = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, m_Target.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, m_LerpSpeed * Time.deltaTime);
    }
}