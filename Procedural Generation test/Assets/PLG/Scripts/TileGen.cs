using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGen : MonoBehaviour
{
    public GameObject[] tile;
    bool spawned = false;

    // Update is called once per frame
    void Update()
    {
        int r = Random.Range(0, tile.Length);
        if(tile[r] != null && !spawned)
        {

          GameObject go =  Instantiate(tile[r], transform);
          Generator.tileDictionary.Add(go.transform.position, go); // populates dictionary
          spawned = true;
        }
    }
}
