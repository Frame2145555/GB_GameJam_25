using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D customCursor; // Drag your cursor texture here
    public Texture2D defaultCursor; // Optional: Default cursor texture
    public Vector2 cursorHotspot = Vector2.zero; // Adjust cursor hotspot if needed

    // Called when the pointer enters the button area

    void start(){Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);}
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
    }

    // Called when the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
