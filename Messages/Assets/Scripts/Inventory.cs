using Photon;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : Photon.MonoBehaviour 
{
	[SerializeField]
	private ItemDatabase m_ItemDatabase;
	[SerializeField]
	private Transform m_SlotsRoot;
	[SerializeField]
	private float m_NumberOfSlots;

	private List<GameObject> m_Slots = new List<GameObject>();
	private List<Item> m_Items = new List<Item>();

	public bool IsWriting { get; set; }

	public List<Item> ItemInSlot
	{
		get 
		{
			return m_Items;
		}
	}

	protected void Awake()
	{
        if (photonView.isMine)
        {
            InitialiseInventory();
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

	protected void Update()
	{
		// Makes sure the UI doesn't move ; TEMP FIX I suppose.
		if (transform.position != Vector3.zero)
		{
			transform.position = Vector3.zero;
		}
	}

    private void InitialiseInventory()
    {
        AddSlots();

        // Debug purposes
        AddItem(0);
    }

	private void AddSlots()
	{
		for (int i = 0 ; i < m_NumberOfSlots ; i++)
		{
			m_Items.Add(new Item());

			GameObject slot = LoadPrefabInInventoryFolder("Slot");
			m_Slots.Add(slot);
			AdjustPrefabTransform(slot, m_SlotsRoot);
		}
	}

	public void AddItem(int a_ID)
	{
		Item itemToAdd = m_ItemDatabase.ItemFetchItemByID(a_ID);

		for (int i = 0 ; i < m_Items.Count ; i++)
		{
			if (m_Items[i].ItemID == -1)
			{
				m_Items[i] = itemToAdd;

				GameObject item = LoadPrefabInInventoryFolder("Letter");

                // TO DO: Avoid getcomponent if possible
                ItemData itemData = item.GetComponent<ItemData>();

                itemData.ItemInstance = itemToAdd;
                itemData.InventorySlot = i;
				item.GetComponent<Image>().sprite = itemToAdd.ItemSprite;
				AdjustPrefabTransform(item, m_Slots[i].transform);
				item.GetComponent<RectTransform>().transform.localPosition = -Vector3.forward;

				// Only as an inspector help
				item.name = itemToAdd.ItemTitle;

				break;
			}
		}
	}

	public void RemoveItem(Letter a_Letter)
	{
        int i = a_Letter.GetComponent<ItemData>().InventorySlot;
        m_Items.RemoveAt(i);
	}

	public void AddExistingItem(GameObject a_GameObject)
	{
		for (int i = 0 ; i < m_Items.Count ; i++)
		{
			if (m_Items[i].ItemID == -1)
			{
				AdjustPrefabTransform(a_GameObject, m_Slots[i].transform);
				break;
			}
		}
	}

	private bool IsInInventory(Item a_Item)
	{
		for (int i = 0; i < m_Items.Count; i++) 
		{
			if (m_Items[i].ItemID == a_Item.ItemID)
			{
				return true;
			}
		}
		return false;
	}
		
	private GameObject LoadPrefabInInventoryFolder(string a_GameObjectName)
	{
		return PhotonNetwork.Instantiate("Prefabs/Inventory/" + a_GameObjectName, Vector3.zero, Quaternion.identity, 0);
	}
		
	private void AdjustPrefabTransform(GameObject a_GameObject, Transform a_Parent)
	{
		a_GameObject.transform.SetParent(a_Parent);
		a_GameObject.transform.localPosition = Vector3.zero;
		a_GameObject.transform.localScale = Vector3.one;
	}
}