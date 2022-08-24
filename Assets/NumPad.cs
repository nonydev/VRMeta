using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumPad : MonoBehaviour
{
	public Text Target;
	public string ToAdd;

	public void OnTriggerEnter(Collider other)
	{
		Target.text += ToAdd;
	}
}
