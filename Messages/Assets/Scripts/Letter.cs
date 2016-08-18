using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Letter : Photon.PunBehaviour, IPunObservable, IPointerDownHandler
{
	[SerializeField]
	private GameObject m_ButtonsRoot;
	[SerializeField]
	private InputField m_InputField;
	[SerializeField]
	private Text m_MessageToRead;

	private bool m_HasMessage;
	private bool m_CanBeEdited = true;

	public Character CharacterReference { get; set;	}

	protected void Start()
	{
		//CharacterReference = transform.root.GetComponent<Character>();
    }

	public void OnPointerDown(PointerEventData a_EventData)
	{
		DeactivateButtons();
		ActivateButtons();
	}

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

	// ******* BUTTONS *******
	public void OnWriteButtonClicked()
	{
		if (m_CanBeEdited)
		{
			WriteMessage();
		}

		DeactivateButtons();
	}

	public void OnSendButtonClicked()
	{
		if (CharacterReference.CanSendLetter && m_HasMessage && m_CanBeEdited)
		{
			SendLetter(photonView.viewID);
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

	// ******* WRITE MESSAGE *******
	private void WriteMessage()
	{
		CharacterReference.GetCharacterInventory.IsWriting = true;

		GameObject inputField = m_InputField.gameObject;

		if (!inputField.activeInHierarchy)
		{
			inputField.SetActive(true);
		}

		m_InputField.ActivateInputField();
	}

	public void CloseInputField()
	{
		CharacterReference.GetCharacterInventory.IsWriting = false;
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

	// ******* READ MESSAGE *******
	private void ReadMessage()
	{
		StartCoroutine(TurnOffMessageReading());

		if (!m_MessageToRead.gameObject.activeInHierarchy)
		{
			m_MessageToRead.gameObject.SetActive(true);
		}
	}

	private IEnumerator TurnOffMessageReading()
	{
		yield return new WaitForSeconds(3f);
		StopReadingMessage();
	}
		
	public void StopReadingMessage()
	{
		if (m_MessageToRead.gameObject.activeInHierarchy)
		{
			m_MessageToRead.gameObject.SetActive(false);
		}
	}

	// ******* SEND MESSAGE *******
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

    /*
    [PunRPC]
    private void test(Inbox inbox, int a_ViewID)
    {
        

        if (photonView.isMine)
        {
            photonView.RPC("test", PhotonTargets.OthersBuffered, a_ViewID);
        }
    }
    */
    
    public void OnPhotonSerializeView(PhotonStream a_Stream, PhotonMessageInfo a_Info)
    {
        Letter letter = CharacterReference.CurrentMailbox.LinkedInbox.LetterInInbox;

        if (a_Stream.isWriting)
        {
            a_Stream.SendNext(letter);
        }
        else
        {
            letter = (Letter)a_Stream.ReceiveNext();
        }
    }
}