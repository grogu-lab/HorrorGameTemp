using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public void DisplayItemSlots(CanvasGroup canvas, bool visible)
    {
        canvas.alpha = visible? 1f: 0f;
        canvas.interactable = visible;
        canvas.blocksRaycasts = !visible;
    }
}
