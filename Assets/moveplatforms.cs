using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class moveplatforms : NetworkBehaviour
{

    public float speed;
    public bool movePlatforms;


    // Start is called before the first frame update
    void Start()
    {
        movePlatforms = false;


        


    }
    private void Update()
    {
        

        MoveWorld();
      
    }
    public void MoveWorld() 
    {
        if (movePlatforms == true)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
    
