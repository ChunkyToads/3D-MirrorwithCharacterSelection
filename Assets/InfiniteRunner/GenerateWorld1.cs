using UnityEngine;
using Mirror;
using System.Collections.Generic;

/*
	Documentation: https://mirror-networking.com/docs/Guides/NetworkBehaviour.html
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class GenerateWorld1 : NetworkBehaviour
{
    public GameObject[] platforms;


    // Start is called before the first frame update
    public void Start()
    {

    Vector3 pos = new Vector3(0, 0, 0);
            //Generates 100 platforms from whatever objects you set in public inspector slots
            for (int i = 0; i < 100; i++)
            {
                int platformNumber = Random.Range(0, platforms.Length);
                Instantiate(platforms[platformNumber], pos, Quaternion.identity);

                //positions objects 40 apart on x axis.
                pos.x += 40;

                if (platforms[platformNumber].tag == "MiddleLevel")
                {
                    pos.y += 2;
                }
                if (platforms[platformNumber].tag == "BottomLevel")
                {
                    pos.y -= 3;
                }
            }
    }
}


