using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    protected void OnTriggerEnter(Collider a_Other)
    {
        Mailbox mailbox = a_Other.GetComponent<Mailbox>();
        if (mailbox != null && !CanSendLetter)
        {
            CurrentMailbox = mailbox;
            CanSendLetter = true;
        }

        Inbox inbox = a_Other.GetComponent<Inbox>();
        if (inbox != null && inbox.LetterInInbox)
        {
            m_Inventory.AddExistingItem(inbox.LetterInInbox.gameObject);
        }
    }

    protected void OnTriggerExit(Collider a_Other)
    {
        Mailbox mailbox = a_Other.GetComponent<Mailbox>();

        if (mailbox != null && CanSendLetter)
        {
            CurrentMailbox = null;
            CanSendLetter = false;
        }
    }
}