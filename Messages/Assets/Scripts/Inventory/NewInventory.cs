using UnityEngine;
using System.Collections;
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
    
    private List<Slot> m_Slots;
    private List<NewItem> m_Items;

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
                AddItem(ref m_ItemOnInitialise[i]);
            }
        }
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.activeInHierarchy)
            {
                ActivateInventory();
            }
            else
            {
                DeactivateInventory();
            }
        }
    }

    private void ActivateInventory()
    {
        gameObject.SetActive(true);
    }

    private void DeactivateInventory()
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

    private void AddItem(ref NewItem a_ItemToAdd)
    {
        for (int i = 0; i < m_Slots.Count; i++)
        {
            if (!m_Slots[i].HasItem)
            {
                m_Slots[i].HasItem = true;
                m_Items.Add(a_ItemToAdd);
                //SetTransform(a_ItemToAdd.transform, transform);
            }
        }
    }

    private void RemoveItem(int a_ItemToRemove)
    {
        m_Slots[a_ItemToRemove].HasItem = false;
        m_Items.RemoveAt(a_ItemToRemove);
    }

    private void SetTransform(Transform a_Transform, Transform a_Parent)
    {
        a_Transform.SetParent(a_Parent);
        a_Transform.position = Vector3.zero;
        a_Transform.rotation = Quaternion.identity;
        a_Transform.localScale = Vector3.zero;
    }

    private GameObject LoadPrefabInInventory(string a_GameObjectName)
    {
        return PhotonNetwork.Instantiate("Prefabs/Inventory/" + a_GameObjectName, Vector3.zero, Quaternion.identity, 0);
    }
}