using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookModifier : Base
{
    public GameObject Spawner;
    public MonoBehaviour CameraRotation;

    public Transform target;
    private float targetDistance;

    public bool RotateMode;
    [SerializeField]
	private float rotationSpeed = 0.01f;

    [SerializeField]
    private float scaleSize = 0.1f;
    [SerializeField]
    private float minScale = 0.3f;

    private Vector3 currentMousePosition, previousMousePosition;

    void LateUpdate()
    {
        bool rightPressed = Input.GetMouseButton(1);
        bool leftPressed = Input.GetMouseButton(0);
        if (leftPressed && target != null)
        {
            ForceDrag();
            return;
        }

        if (rightPressed && target != null)
        {
            CameraRotation.enabled = false;
			Vector3 mouseDelta = previousMousePosition - Input.mousePosition;
            target.Rotate(cachedTransform.right, -mouseDelta.y * rotationSpeed, Space.World);
            target.Rotate(cachedTransform.up, mouseDelta.x * rotationSpeed, Space.World);
        }

        if (target != null) {
            Vector3 scale = target.localScale + Vector3.one * Input.mouseScrollDelta.y * scaleSize;
            if(scale.magnitude < minScale)
            {
                scale = scale.normalized * minScale;
			}
            target.localScale = scale;
        }

        RaycastHit info;
        Vector3 origin = cachedTransform.position;

        Debug.DrawLine(origin, origin + cachedTransform.forward * 3, Color.red, 5);
        if (!leftPressed && !rightPressed && Physics.Raycast(origin, cachedTransform.forward, out info, 3f, 1 << LayerMask.NameToLayer("Medical")))
        {
            target = info.collider.transform;
            Vector3 targetPos = target.position;
            Broadcast("ObjectSelected", target);
            targetDistance = Vector3.Distance(origin, targetPos);
            Spawner.SetActive(false); 
		} else if(!leftPressed && !rightPressed) {
            target = null;

            Broadcast("ObjectDeselected", null);
            Spawner.SetActive(true);
		}

        if(!rightPressed)
        {
            CameraRotation.enabled = true;
        }

        previousMousePosition = Input.mousePosition;
    }

    private void ForceDrag()
    {
        Vector3 origin = cachedTransform.position;
        Vector3 targetPos = target.position;
        target.position = cachedTransform.position + cachedTransform.forward * targetDistance;
    }
}
