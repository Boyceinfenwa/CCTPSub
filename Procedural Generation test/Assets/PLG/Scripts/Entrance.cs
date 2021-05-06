using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{

    public GameObject player;
    public bool playerspawned = false;
    public Transform spawnpoint;
    // Update is called once per frame
    void Start()
    {
        if(Generator.readyforplayer && !playerspawned)
        {
            // spawn player
            Instantiate(player, spawnpoint);
            playerspawned = true;
        }
    }
}
