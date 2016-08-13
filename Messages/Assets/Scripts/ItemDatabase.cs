using LitJson;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour 
{
	private JsonData m_ItemData;
	private List<Item> m_Database = new List<Item>();

	protected void Awake()
	{
		m_ItemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		BuildItemDatabase();
	}

	public Item ItemFetchItemByID(int a_ID)
	{
		int i;

		for (i = 0; i < m_Database.Count ; i++)
		{
			if (m_Database[i].ItemID == a_ID)
			{
				return m_Database[i];
			}
		}

		return null;
	}

	private void BuildItemDatabase()
	{
		for (int i = 0 ; i < m_ItemData.Count ; i++)
		{
			m_Database.Add(new Item(
				(int)m_ItemData[i]["id"],
				m_ItemData[i]["title"].ToString(),
				(bool)m_ItemData[i]["stackable"],
				(bool)m_ItemData[i]["hasBeenSent"],
				m_ItemData[i]["message"].ToString(),
				m_ItemData[i]["description"].ToString(),
				m_ItemData[i]["sprite"].ToString()));
		}
	}
}

public class Item
{
	public int ItemID { get; set; }
	public string ItemTitle { get; set; }
	public bool ItemStackable { get; set; }
	public bool ItemHasBeenSent { get; set; }
	public string ItemMessage { get; set; }
	public string ItemDescription { get; set; }
	public string ItemSpriteName;
	public Sprite ItemSprite { get; set; }

	public Item(int a_ID, string a_Title, bool a_Stackable, bool a_ItemHasBeenSent, string a_ItemMessage, string a_ItemDescritpion, string a_ItemSpriteName)
	{
		this.ItemID = a_ID;
		this.ItemTitle = a_Title;
		this.ItemStackable = a_Stackable;
		this.ItemHasBeenSent = a_ItemHasBeenSent;
		this.ItemMessage = a_ItemMessage;
		this.ItemDescription = a_ItemDescritpion;
		this.ItemSprite = Resources.Load<Sprite>("Sprites/Items/" + a_ItemSpriteName);
	}
		
	public Item()
	{
		this.ItemID = -1;	
	}
}