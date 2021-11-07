using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorType
{
    Pointer = 0, Grab = 1
}

public class CursorAnimator : MonoBehaviour
{
    [SerializeField] private Texture2D[] cursors;
    [SerializeField] private CursorType cursorType;

    public void SetCursor(CursorType type)
    {
        Cursor.SetCursor(cursors[(int)type], new Vector2(32, 45), CursorMode.Auto);
    }
}
