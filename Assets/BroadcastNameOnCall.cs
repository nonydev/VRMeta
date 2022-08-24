using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastNameOnCall : Base
{
    public string Incoming;
	public string Outgoing;
	private void Awake()
	{
		CacheMethod(Incoming, (o)=>
		{
			BroadcastName();
		});
	}

	public void BroadcastName()
	{
		Broadcast(Outgoing, name);
	}
}
