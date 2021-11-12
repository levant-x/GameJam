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
    [SerializeField] private bool useGrabOnMouseDown = true;

    public void SetCursor(CursorType type)
    {
        Cursor.SetCursor(cursors[(int)type], new Vector2(15, 10), CursorMode.ForceSoftware);
    }

    private void Update()
    {
        if (!useGrabOnMouseDown) return;
        if (Input.GetMouseButton(0)) SetCursor(CursorType.Grab);
        else SetCursor(CursorType.Pointer);
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
