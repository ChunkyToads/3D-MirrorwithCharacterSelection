using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteRunner : MonoBehaviour
{
    public float speed;
    public float sideSpeed;
    public float BoostSpeed;
    public Text distancemoved;
    public Rigidbody rigid;
    public float jumpForce;
    public bool isGrounded = false;
    Vector3 movement;
    Vector3 movement1;
    
   

    // Start is called before the first frame update
    public void Start()
    {
        rigid = GetComponent<Rigidbody>();
        InvokeRepeating("distance", 0, 1 / speed);
        jumpForce = 2;
       
        
    }
   
    public void distance()
    {
        //distancemoved.text displays as current position on x axis!
        distancemoved.text = "Distance" + transform.position.x.ToString();
    }
   
    public void Update()
    {
   
        transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        transform.position += movement * Time.deltaTime * sideSpeed;
        transform.position += movement1 * Time.deltaTime * BoostSpeed;

        movement.z = Input.GetAxisRaw("Horizontal") * -1.0f;
        

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true || Input.GetKeyDown("joystick button 0") && isGrounded == true)
        {
            rigid.velocity = new Vector3(0, 3, 0) * jumpForce;
           
        }
        
            //if up arrow is pushed characters speed increases
            movement1.x = Input.GetAxisRaw("Vertical");
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "ground")
        {

            isGrounded = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ground")
        {
            isGrounded = false;

        }

        
    }
}
