using UnityEngine;
using System.Collections.Generic;

public class Inbox : MonoBehaviour
{
	public Letter PossessedLetter { get; set; }

    protected void OnTriggerEnter(Collider a_Other)
    {
        CharacterCollisions characterCollisions = a_Other.GetComponent<CharacterCollisions>();
        if (characterCollisions != null)
        {
            characterCollisions.Inventory.AddExistingItem(PossessedLetter);
            PossessedLetter = null;
        }
    }
}