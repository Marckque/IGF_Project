using UnityEngine;

public class ItemData : MonoBehaviour
{
	public int Amount { get; set; }
    public int InventorySlot { get; set; }
	public Item ItemInstance { get; set; }

	void Awake()
	{
		Amount = 1;
	}
}