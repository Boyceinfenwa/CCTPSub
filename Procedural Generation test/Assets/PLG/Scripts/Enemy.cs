using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayerController
{
    // Start is called before the first frame update
    public int speed;
    public Transform edge;
    bool moveR;

    void Start()
    {
        moveR = Random.Range(0, 2) == 0;
    }

     void Update()
    {
        Vector2 movement = Vector2.zero;
        if(moveR)
        {
            movement.x = 1;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            
        }
        else
        {
            movement.x = -1;
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }

        RaycastHit2D ray = Physics2D.Raycast(edge.position, Vector2.down, 1f);
        if(ray.collider == false || ray.collider.gameObject.layer == LayerMask.NameToLayer("NoColl"))
        {
            moveR = !moveR;
        }

          movepos = movement * speed;
        transform.position += movepos * Time.deltaTime * speed;

    }

     void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            moveR = !moveR;
        }
           
    }
    
}
