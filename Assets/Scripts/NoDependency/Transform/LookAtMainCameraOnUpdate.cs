using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class LookAtMainCameraOnUpdate : Base
{
	void Update()
	{
		cachedTransform.LookAt(Camera.main.transform);
	}
}
