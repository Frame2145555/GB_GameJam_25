using UnityEngine;
using UnityEngine.EventSystems;

public class CursorOnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Texture2D hoverCursor;    // Cursor texture when hovering
    public Texture2D clickCursor;    // Cursor texture when clicked
    public Texture2D defaultCursor;  // Default cursor texture
    public Vector2 cursorHotspot = Vector2.zero; // Adjust hotspot if needed

    // When the pointer enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(hoverCursor, cursorHotspot, CursorMode.Auto);
    }

    // When the pointer exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    // When the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Change to the click cursor
        Cursor.SetCursor(clickCursor, cursorHotspot, CursorMode.Auto);

        // Optional: Reset to hover cursor after a short delay
        Invoke("ResetToHoverCursor", 0.1f);
    }

    // Resets the cursor to the hover state
    private void ResetToHoverCursor()
    {
        Cursor.SetCursor(hoverCursor, cursorHotspot, CursorMode.Auto);
    }
}
