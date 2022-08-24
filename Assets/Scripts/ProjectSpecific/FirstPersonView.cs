using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView : Base
{
    private Vector3 previousMousePosition;
    private Vector3 rotation;

    private Vector2 yLock = new Vector2(-60, 60);

	protected override void OnEnable()
	{
        base.OnEnable();
        Vector3 currentMousePosition = Input.mousePosition;
        previousMousePosition = currentMousePosition;
    }

    private void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        if (Cursor.visible == false)
        {
            Vector3 delta = currentMousePosition - previousMousePosition;
            rotation += new Vector3(-delta.y, delta.x, 0);
        }

        rotation.x = Mathf.Clamp(rotation.x, yLock.x, yLock.y);
        cachedTransform.rotation = Quaternion.Euler(rotation);

        previousMousePosition = currentMousePosition;
    }
}
