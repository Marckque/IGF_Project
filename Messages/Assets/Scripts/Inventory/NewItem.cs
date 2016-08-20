using UnityEngine.EventSystems;

public class NewItem : Photon.PunBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public NewInventory CharacterInventory { get; set; }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
    }
}