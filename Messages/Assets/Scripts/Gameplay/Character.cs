using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Inventory m_Inventory;

    public bool CanSendLetter { get; set; }
    public Mailbox CurrentMailbox { get; set; }

    public Inventory GetCharacterInventory
    {
        get { return m_Inventory; }
    }
}