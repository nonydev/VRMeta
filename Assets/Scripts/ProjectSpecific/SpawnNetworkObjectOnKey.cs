using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNetworkObjectOnKey : Base
{
    public string ObjectName;
    public KeyCode Key;

    public bool OnUp;
    public bool OnDown;

    void Update()
    {
        if(OnUp && Input.GetKeyUp(Key) || OnDown && Input.GetKeyDown(Key))
        {
            GameObject go = PhotonNetwork.Instantiate(ObjectName, CachedTransform.position + CachedTransform.forward, Quaternion.identity);
        }
    }
}
