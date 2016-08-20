using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Letter : NewItem, IPunObservable
{
	[SerializeField]
	private GameObject m_ButtonsRoot;
	[SerializeField]
	private InputField m_InputField;
	[SerializeField]
	private Text m_MessageToRead;

    private Mailbox m_Mailbox;
	private bool m_HasMessage;
	private bool m_IsReceivedLetter;

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
        if (m_Mailbox != null)
        {
            SendLetter(photonView.viewID);
        }
        else
        {
            Debug.LogWarning("Player tries to send " + name + " but there is no Mailbox");
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
    // Doesn't work: It is UI so it will never be called as the box collider will be on the UI.
    protected void OnTriggerEnter(Collider a_Other)
    {
        Mailbox other = a_Other.GetComponent<Mailbox>();

        if (other != null)
        {
            m_Mailbox = other;
        }
    }

    // Doesn't work: It is UI so it will never be called as the box collider will be on the UI.
    protected void OnTriggerExit(Collider a_Other)
    {
        Mailbox other = a_Other.GetComponent<Mailbox>();

        if (other != null)
        {
            m_Mailbox = null;
        }
    }

    private void SendLetter(int a_ViewID)
	{
        // Temporary: shows that it is a received letter
        GetComponent<Image>().color = Color.red;

        Inbox inbox = m_Mailbox.LinkedInbox;
        inbox.Letter = this;
        transform.SetParent(inbox.transform);
        inbox.Letter.transform.position = Vector3.zero;

        //RemoveItem(this);

        //test(inbox, a_ViewID);
    }

    /*
    [PunRPC]
    private void test(Inbox inbox, int a_ViewID)
    {
        if (photonView.isMine)
        {
            photonView.RPC("test", PhotonTargets.OthersBuffered, a_ViewID);
        }
    }

    private void SendLetter(int a_ViewID)
	{
        // Temporary but shows that you've received the letter
        GetComponent<Image>().color = Color.red;

		m_CanBeEdited = false;
		Inbox inbox = CharacterReference.CurrentMailbox.LinkedInbox;
        inbox.LetterInInbox = this;
        transform.SetParent(inbox.transform);
        transform.position = Vector3.zero;
        CharacterReference.GetCharacterInventory.RemoveItem(this);

        //test(inbox, a_ViewID);
    }

    */

    public void OnPhotonSerializeView(PhotonStream a_Stream, PhotonMessageInfo a_Info)
    {
        Letter letter = CharacterReference.CurrentMailbox.LinkedInbox.Letter;

        if (a_Stream.isWriting)
        {
            a_Stream.SendNext(letter);
        }
        else
        {
            letter = (Letter)a_Stream.ReceiveNext();
        }
    }
    #endregion
}