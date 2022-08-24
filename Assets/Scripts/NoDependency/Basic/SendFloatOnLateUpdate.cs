﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFloatOnLateUpdate : Base {
	public float Value;
	public string CallSetValue;
	public string Outgoing;

	private void Awake()
	{
		CacheMethod(CallSetValue, SetValue);
	}

	private void SetValue(object o)
	{
		float f;
		if(o is float) 
		{
			Value = (float) o;
		} else if(float.TryParse(o.ToString(), out f)) 
		{
			Value = f;
		}
	}

	private void LateUpdate()
	{
		Call(Outgoing, cachedGameObject, Value);
	}
}
