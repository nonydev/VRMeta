using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideRPC : MonoBehaviour
{
    public PhotonView view;
    [PunRPC]
    public void Suicide()
    {
        if(view.AmOwner) {
            PhotonNetwork.Destroy(gameObject);
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
