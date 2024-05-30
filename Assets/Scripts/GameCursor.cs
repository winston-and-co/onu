using UnityEngine;

public class GameCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // Assign this in the Inspector
    public Vector2 hotspot = Vector2.zero; // Customize the hotspot; zero means the top left corner of the image

    void Start()
    {
        SetCustomCursor();
        DontDestroyOnLoad(gameObject);
    }

    void SetCustomCursor()
    {
        // Set the cursor to the texture with the hotspot defined
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);

        // Optionally, hide the hardware cursor if you want the custom cursor only
        Cursor.visible = true;
    }

    void OnDestroy()
    {
        // Reset the cursor to the default
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
