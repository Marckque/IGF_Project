using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Letter : NewItem
{
    #region Variables
    [SerializeField]
	private GameObject m_ButtonsRoot;
	[SerializeField]
	private InputField m_InputField;
	[SerializeField]
	private Text m_MessageToRead;

    private bool m_HasMessage;
    private Mailbox m_Mailbox;
	private bool m_IsReceivedLetter;
    #endregion

    public Character CharacterReference { get; set;	}

    #region Events
    public override void OnPointerDown(PointerEventData a_EventData)
	{
		DeactivateButtons();
	}

    public override void OnPointerEnter(PointerEventData a_EventData)
    {
        ActivateButtons();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        DeactivateButtons();
    }
    #endregion Events

    #region Buttons
    private void ActivateButtons()
    {
        if (!m_ButtonsRoot.activeInHierarchy)
        {
            m_ButtonsRoot.SetActive(true);
        }
    }

    private void DeactivateButtons()
    {
        if (m_ButtonsRoot.activeInHierarchy)
        {
            m_ButtonsRoot.SetActive(false);
        }
    }

    public void OnWriteButtonClicked()
	{
        if (!m_IsReceivedLetter)
        {
            WriteMessage();
        }

		DeactivateButtons();
	}

	public void OnSendButtonClicked()
	{
        SetMailbox();

        if (m_Mailbox != null)
        {
            SendLetter(photonView.viewID);
        }
        else
        {
            Debug.LogWarning("Player tries to send " + name + " but there is no Mailbox.");
        }

        DeactivateButtons();
	}

	public void OnReadButtonClicked()
	{
		if (m_HasMessage)
		{
			ReadMessage();
		}

		DeactivateButtons();
	}
    #endregion

    #region Write
    private void WriteMessage()
	{
		GameObject inputField = m_InputField.gameObject;
		if (!inputField.activeInHierarchy)
		{
			inputField.SetActive(true);
		}

		m_InputField.ActivateInputField();
	}

	public void StopWriteMessage()
	{
		m_InputField.DeactivateInputField();
		m_MessageToRead.text = m_InputField.text;

		if (m_MessageToRead.text != "")
		{
			m_HasMessage = true;
		}

		GameObject inputField = m_InputField.gameObject;
		if (inputField.activeInHierarchy)
		{
			inputField.SetActive(false);
		}
	}
    #endregion

    #region Read
    private void ReadMessage()
	{
        GameObject message = m_MessageToRead.gameObject;
        if (!message.activeInHierarchy)
        {
            message.SetActive(false);
            StartCoroutine(TurnOffMessageReading());
        }
	}
		
	public void StopReadMessage()
	{
        GameObject message = m_MessageToRead.gameObject;
		if (message.activeInHierarchy)
		{
            message.SetActive(false);
		}
	}

    private IEnumerator TurnOffMessageReading()
    {
        yield return new WaitForSeconds(3f);
        StopReadMessage();
    }
    #endregion

    #region Send
    private void SetMailbox()
    {
        CharacterCollisions characterCollisions = CharacterInventory.CollidesWith;
        Collider collider = characterCollisions.GetCurrentCollision();

        if (collider != null)
        {
            Mailbox mailbox = collider.GetComponent<Mailbox>();
            m_Mailbox = mailbox;
        }
    }

    private void SendLetter(int a_ViewID)
	{
        // Temporary: shows that it is a received letter
        GetComponent<Image>().color = Color.red;

        Inbox inbox = m_Mailbox.LinkedInbox;
        if (inbox.PossessedLetter == null)
        {
            inbox.PossessedLetter = this;
            transform.SetParent(inbox.transform);
            inbox.PossessedLetter.transform.position = Vector3.zero;

            CharacterInventory.RemoveItem(this);
        }
        else
        {
            Debug.LogWarning(inbox.name + " already has a letter.");
        }
    }
    #endregion
}