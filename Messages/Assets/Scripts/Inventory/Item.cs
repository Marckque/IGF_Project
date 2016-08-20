using UnityEngine.EventSystems;

public class Item : Photon.PunBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Inventory CharacterInventory { get; set; }

    #region Events
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
    }
    #endregion
}