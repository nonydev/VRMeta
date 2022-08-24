using UnityEngine;
using System.Collections;

public class SetActiveMonoBehaviourOnCall : Base
{
	[UnityEngine.Serialization.FormerlySerializedAs("target")]
	public MonoBehaviour Target;
	[UnityEngine.Serialization.FormerlySerializedAs("callEnable")]
	public string CallEnable;
	[UnityEngine.Serialization.FormerlySerializedAs("callDisable")]
	public string CallDisable;
	[UnityEngine.Serialization.FormerlySerializedAs("callSetActive")]
	public string CallSetActive;

	private void Awake()
	{
		CacheMethod(CallEnable, Enable);
		CacheMethod(CallDisable, Disable);
		CacheMethod(CallSetActive, SetActive);
	}

	private void Enable(object o)
	{
		if (Target == null)
		{
			return;
		}
		Target.enabled = (true);
	}

	private void Disable(object o)
	{
		if (Target == null)
		{
			return;
		}
		Target.enabled = (false);
	}

	private void SetActive(object o)
	{
		if (Target == null)
		{
			return;
		}
		if (o is string)
		{
			o = bool.Parse((string)o);
		}

		if (o is bool)
		{
			Target.enabled = ((bool)o);
		}

	}
}
