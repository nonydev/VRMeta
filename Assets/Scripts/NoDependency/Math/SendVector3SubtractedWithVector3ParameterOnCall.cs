﻿using UnityEngine;
using System.Collections;

public class SendVector3SubtractedWithVector3ParameterOnCall : Base
{

	public string CallSend;
	public string Outgoing;
	public string CallSetValue;

	public Vector3 Value;

	private void Awake()
	{
		UpdateCachedFields();
		CacheMethod(CallSetValue, SetValue);
		CacheMethod(CallSend, Add);
	}

	private void SetValue(object o)
	{
		if (o is Vector3)
		{
			Value = (Vector3)o;
		}
	}

	private void Add(object o)
	{
		if (o is Vector3)
		{
			Call(Outgoing, cachedGameObject, Value - ((Vector3)o));
		}
	}
}