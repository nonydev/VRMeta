using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNetworkObjectOnMouse : Base
{
    public string ObjectName;
    public string CallChangeName;
    public int Button;

    public bool OnUp;
    public bool OnDown;

    private void Awake()
    {
        CacheMethod(CallChangeName, ChangeName);
	}

    private void ChangeName(object o)
    {
        ObjectName = o.ToString();
	}

    void Update()
    {
        if (OnUp && Input.GetMouseButtonUp(Button) || OnDown && Input.GetMouseButtonDown(Button))
        {
            GameObject go = PhotonNetwork.Instantiate(ObjectName, CachedTransform.position + CachedTransform.forward, Quaternion.identity);
        }
    }
}
