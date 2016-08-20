using UnityEngine;
using System.Collections;

public class CharacterCollisions : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private NewInventory m_Inventory;

    private Collider m_IsCollidingWith;
    #endregion

    public NewInventory Inventory
    {
        get
        {
            return m_Inventory;
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