using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Telporting : NetworkBehaviour
{

    public Transform teleportTarget;
    public GameObject player;

    public void Update()
    {

    }

        void OnCollisionEnter2D(Collision2D collision)
        { 

        //Only base.isClient is necessary here for some reason. If so everything works all characters, whether
        //client or host.  DO NOT attempt to add base.HasAuthority here! Even though you did/needed to in other
        //code elsewhere.  If you try that here it teleportation stops working on some characters on client end
        //not sure why? Oh well. 

            if (base.isClient)
                if (collision.gameObject.name == "DeathCollider")
                { player.transform.position = teleportTarget.transform.position; }
        }
   
}


    

