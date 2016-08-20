using UnityEngine;
using System.Collections;

public class CharacterCollisions : MonoBehaviour
{
    private Collider m_IsCollidingWith;

    public Collider CollidesWith
    {
        get
        {
            return m_IsCollidingWith;
        }
    }

    #region OnTrigger functions (interactible collisions)
    protected void OnTriggerEnter(Collider a_Collider)
    {
        m_IsCollidingWith = a_Collider;
    }

    protected void OnTriggerExit(Collider a_Collider)
    {
        m_IsCollidingWith = null;
    }

    public Collider GetCurrentCollision()
    {
        return m_IsCollidingWith;
    }
    #endregion
}