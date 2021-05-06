using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{

    public GameObject[] rooms;
    public GameObject background;
    public GameObject springPad;
    bool flag =false;

    // Update is called once per frame
    void Update()
    {
        // checks if tiles are generated
        if(Generator.stageOneComp && !flag)
        {
            flag = true;
            // Gen Room

            int r = Random.Range(0, rooms.Length);
            Instantiate(rooms[r], transform);
            Instantiate(background, transform);

            Instantiate(springPad, transform);
         
        }
    }
}
