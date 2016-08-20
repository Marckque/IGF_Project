using UnityEngine;

public class Inbox : MonoBehaviour
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
}