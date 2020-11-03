using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : NetworkBehaviour
{
    public List<Transform> targets;
    

    public Vector3 offset;
    public float smoothTime = .5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    //These variables are used to save a reference to a specific mesh or sprite
    //first as its transform ie 'playerClone' and then converted into the actual object ie 'obj' as it instantiates 
    //over the network. 
    private Transform playerClone;
    private GameObject obj;
    private Transform playerClone2;
    private GameObject obj2;

  

    private void Start()
    {
        cam = GetComponent<Camera>();
        targets = new List<Transform>();
        
    }

    void LateUpdate() 
    {
        // Here is where we take the aformentioned variables storing 
        //transform and then object of specific mesh/sprites. SEARCHING 
        //for specific named prefabs "" that may not be spawned yet... 
        //Then if/when they are found in scene (when network fills each players 
        //seleccted characters... it AddObjectsToList() aka puts 
        //prefabs to empty target slots of MultipleTargetCamera Component Script
        obj = GameObject.Find("Runner10000(Clone)");
        playerClone = obj.GetComponent<Transform>();

        obj2 = GameObject.Find("Runner10111(Clone)");
        playerClone2 = obj2.GetComponent<Transform>();



        
            AddObjectsToList();
        


        //List<Transform> targets = new List<Transform>() { playerClone };

        if (targets.Count == 0)
            return;


        Move();
        Zoom();


    }
    void AddObjectsToList()
    {
        //if not already in slot atleast once, this adds the prefabs to the empty slots for the
        //camera to calculate between and follow the middle point between then later in code. The if (!targets.Contains(playerClone)) 
        //also STOPS it from continually adding prefab over and over to empty slots!!! targets.Insert(0, playerClone) sets prefab into specific 
        //empty element slot (which is 1 of 2 things required to keep MultipleTargetCamera script working)
        if (!targets.Contains(playerClone))
            targets.Insert(0, playerClone);
       

        if (!targets.Contains(playerClone2))
            targets.Insert(1, playerClone2);

        // This is the 2nd half of what is REQUIRED to keep MultipleTargetCamera script working properly... this 
        //'for loop' basically reverse cycles through the other existing forloop checking for any 
        //empty element slots (which will occur if players disconnect even momentarily) then if empty
        //slots are found targets.RemoveAt(i) deletes them... allowing the players to rejoin and not disturb 
        //MultipleTargetCamera script from working
        for (var i = targets.Count - 1; i > -1; i--)
        {
            if (targets[i] == null)
                targets.RemoveAt(i);
        }
        return;



    }

    void Zoom()
    {
      
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    void Move() 
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    float GetGreatestDistance() 
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) 
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }
    Vector3 GetCenterPoint()
    { 
        if(targets.Count == 1) 
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) 
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
