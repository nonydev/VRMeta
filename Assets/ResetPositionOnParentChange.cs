using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionOnParentChange : MonoBehaviour
{
	private void OnTransformParentChanged()
	{
		transform.localPosition = new Vector3();	
	}
}
