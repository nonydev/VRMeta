using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursorVisibilityOnClick : Base
{
    private const string CursorVisibleMessage = "OnCursorVisible";
    private const string CursorInvisibleMessage = "OnCursorInvisible";
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = !Cursor.visible;
            Broadcast(Cursor.visible ? CursorVisibleMessage : CursorInvisibleMessage, null);
        }
    }
}
