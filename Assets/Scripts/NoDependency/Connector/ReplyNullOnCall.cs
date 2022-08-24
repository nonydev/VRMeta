﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ReplyNullOnCall : Base {
	public string CallReply;
	public string Outgoing;

	private void Awake()
	{
		CacheMethod(CallReply, Reply);
	}

	private void Reply(object o)
	{
		KeyValuePair<Type, object> pair = CallStack.Peek();
		if(pair.Key.IsSubclassOf(typeof(Base)) || pair.Equals(typeof(Base))) {
			ReplyToBase((Base) pair.Value);
		} else if(pair.Key.IsSubclassOf(typeof(MonoBehaviour)) || pair.Equals(typeof(MonoBehaviour))) {
			ReplyToMonoBehaviour((MonoBehaviour) pair.Value);
		} else if(pair.Key.IsSubclassOf(typeof(MonoBehaviour)) || pair.Equals(typeof(MonoBehaviour))) {
			ReplyToGameObject((GameObject) pair.Value);
		}
	}

	private void ReplyToBase(Base b)
	{
		GameObject cgo = GetCachedGameObject(b);
		Call(Outgoing, cgo);
	}

	private void ReplyToGameObject(GameObject go)
	{
		Call(Outgoing, go);
	}

	private void ReplyToMonoBehaviour(MonoBehaviour mono)
	{
		Call(Outgoing, mono.gameObject);
	}
}
