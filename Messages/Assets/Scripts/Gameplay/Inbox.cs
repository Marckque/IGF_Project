using UnityEngine;

public class Inbox : MonoBehaviour//, IPunObservable
{
	public Letter PossessedLetter { get; set; }

    protected void OnTriggerEnter(Collider a_Other)
    {
        CharacterCollisions characterCollisions = a_Other.GetComponent<CharacterCollisions>();
        if (PossessedLetter != null && characterCollisions != null)
        {
            characterCollisions.Inventory.AddExistingItem(PossessedLetter);
            PossessedLetter = null;
        }
    }

    /*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(PossessedLetter);
        }
        else
        {
            this.PossessedLetter = (Letter)stream.ReceiveNext();
        }
    }*/
}