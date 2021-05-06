using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float gravity = 0.0f;
    float speed = 5f;
    protected Vector3 movepos;
    bool jump = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
    }

    void MovePlayer()
    {
        movepos = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movepos * Time.deltaTime * speed;
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space ))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector2(0f, 5f), ForceMode.Impulse);
          
        }
    }

    
}
