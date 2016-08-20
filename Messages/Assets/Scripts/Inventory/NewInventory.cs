using UnityEngine;
using System.Collections.Generic;

public class NewInventory : Photon.PunBehaviour
{
    [Header("Slot parameters")]
    [SerializeField]
    private Slot m_Slot;
    [SerializeField]
    private Transform m_SlotsRoot;
    [SerializeField]
    private int m_NumberOfSlots;

    [Header("Item parameters")]
    [SerializeField]
    private NewItem[] m_ItemOnInitialise;

    [Header("Interaction with environment")]
    [SerializeField]
    private CharacterCollisions m_CharacterCollisions;

    private List<Slot> m_Slots;
    private List<NewItem> m_Items;

    public CharacterCollisions CollidesWith
    {
        get
        {
            return m_CharacterCollisions;
        }
    }

    protected void Start()
    {
        InitialiseInventory();
        DeactivateInventory();
    }

    private void InitialiseInventory()
    {
        m_Slots = new List<Slot>();
        m_Items = new List<NewItem>();

        AddSlot(m_NumberOfSlots);

        if (m_ItemOnInitialise.Length > 0)
        {
            for (int i = 0; i < m_ItemOnInitialise.Length; i++)
            {
                AddItem(m_ItemOnInitialise[i]);
            }
        }
    }

    public void ActivateInventory()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateInventory()
    {
        gameObject.SetActive(false);
    }

    private void AddSlot(int a_AmountToAdd)
    {
        for (int i = 0; i < a_AmountToAdd; i++)
        {
            GameObject slot = LoadPrefabInInventory(m_Slot.name);
            Slot slotReference = slot.GetComponent<Slot>();
            m_Slots.Add(slotReference);

            SetTransform(m_Slots[i].transform, m_SlotsRoot);
        }
    }

    public void AddItem(NewItem a_Item)
    {
        for (int i = 0; i < m_Slots.Count; i++)
        {
            if (m_Slots[i].Item == null)
            {
                m_Slots[i].Item = a_Item;

                GameObject item = LoadPrefabInInventory(a_Item.name);
                NewItem itemReference = item.GetComponent<NewItem>();
                itemReference.CharacterInventory = this;
                m_Items.Add(itemReference);
                SetTransform(m_Items[i].transform, m_Slots[i].transform);

                return;
            }
        }
    }

    public void RemoveItem(NewItem a_Item)
    {
        for (int i = 0; i < m_Slots.Count; i++)
        {
            if (m_Slots[i].Item != null && m_Items[i] == a_Item)
            {
                m_Slots[i].Item = null;
                m_Items.RemoveAt(i);
                return;
            }
        }
    }

    private void SetTransform(Transform a_Transform, Transform a_Parent)
    {
        a_Transform.SetParent(a_Parent);
        a_Transform.position = Vector3.zero;
        a_Transform.rotation = Quaternion.identity;
        a_Transform.localScale = Vector3.one;
    }

    private GameObject LoadPrefabInInventory(string a_GameObjectName)
    {
        return PhotonNetwork.Instantiate("Prefabs/Inventory/" + a_GameObjectName, Vector3.zero, Quaternion.identity, 0);
    }
}