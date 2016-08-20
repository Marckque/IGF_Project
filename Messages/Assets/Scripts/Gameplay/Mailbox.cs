using UnityEngine;

public class Mailbox : MonoBehaviour 
{
	[SerializeField]
	private Inbox m_Inbox;

	public Inbox LinkedInbox
	{
		get
		{
			return m_Inbox;
		}
	}
}