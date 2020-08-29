using UnityEngine;

/// <summary>
/// Component to hold every Settings regarding the Cursor
/// </summary>
public class CursorSettings : MonoBehaviour
{
    static CursorSettings cursorSettings = null;

    [SerializeField] Texture2D normalSprite = null;

    void Awake()
    {
        if (!cursorSettings)
        {
            cursorSettings = this;
            Cursor.visible = true;
        }
        else
        {
            Debug.LogError("Multiple CursorSetting found on: " + gameObject.name);
        }
    }
    /// <summary>
    /// Resets the Cursor to its initial Sprite
    /// </summary>
    private void Reset()
    {
        if (normalSprite == null)
        {
            Debug.LogError("Missing Sprite in " + this);
        }
        else
        {
            Cursor.SetCursor(normalSprite, Vector2.zero, CursorMode.Auto);
        }
    }
}
