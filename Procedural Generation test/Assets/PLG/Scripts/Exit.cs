using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    public GameObject player;
    Collider2D playerCol;
    void Start()
    {
       playerCol = player.GetComponent<Collider2D>();
    }
    
    public void NLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
       
        
            Debug.Log("EXIT");
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
               NLevel();
            }
        
     
    }

}
