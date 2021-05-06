using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Generator : MonoBehaviour
{
    public int length;
    public int height;
    public int scale;
    public bool rSeed = true;

    public GameObject[] startingRooms;
    public GameObject[] paths; // rooms on the path 
    public GameObject[] fillaRooms;
    public GameObject[] endRoom;

    public GameObject[,] roomArray;
    public List<Vector2> loadedR;
    public int delayTime;

    public GameObject[] treasure;
    public GameObject[] enemies;
    public GameObject[] traps;
    public GameObject border;

    public int genMethod = 0;

    public static Dictionary<Vector2, GameObject> tileDictionary;

    public int difficulty = 0; // 1 easy , 2 mormal ;, 3 hard 0deafault 

    public GameObject pathchecker;
    bool pc = false;
    public static bool stageOneComp = false;
    public static bool stageTwoComp = false;

    public static bool readyforplayer = true;
    int dir;
    int delay = 0;
    public int seed;
    bool random = false;

     void Start()
    {
        InitGen();
    }

     void Update()
    {
        switch(genMethod)
        {
            case 0:
                MethodOne();
                break;
            case 1:
                MethodTwo();
                break;
            case 2:
                MethodOne();
                break;
        }
       
        
    }

    void MethodOne()
    {
        if (!stageOneComp)
        {
            Instantiate(border);
            if (delay >= delayTime)
            {
                delay = 0;
                if (dir == 0 || dir == 1) // rooms moving right
                {
                    if (transform.position.x < length - 1)
                    {
                        transform.position += Vector3.right;
                        int r = Random.Range(0,3);
                        GenerateRoom(paths[r]);
                        dir = Random.Range(0, 5);
                        if (dir == 2)
                        {
                            dir = 1;
                        }
                        else if (dir == 3)
                        {
                            dir = 4;
                        }
                    }

                    else
                        dir = 4;
                }

                else if (dir == 2 || dir == 3) // rooms moving left 
                {
                    if (transform.position.x > 0)
                    {
                        transform.position += Vector3.left;
                        int r = Random.Range(0, 3);
                        GenerateRoom(paths[r]);
                        dir = Random.Range(0, 5);
                        if (dir == 0)
                            dir = 2;
                        else if (dir == 1)
                            dir = 4;
                    }
                    else dir = 4;
                }



                else if (dir == 4)
                {
                    if (transform.position.y > -height + 1) // moving down
                    {
                        Destroy(GetRoom(transform.position));
                        int r = (Random.Range(0, 2) == 0) ? 1 : 3;
                        GenerateRoom(paths[r]);
                        transform.position += Vector3.down;
                        int rand = Random.Range(0, 4);
                        if (rand == 0)
                        {
                            if (transform.position.y > -height + 1)
                            {
                                GenerateRoom(paths[3]);
                                transform.position += Vector3.down;

                            }
                            else
                            {
                                dir = 4;
                                return;
                            }

                        }
                        int ran = Random.Range(2, 4);
                        GenerateRoom(paths[ran]);
                        if (transform.position.x == 0)
                        {
                            dir = 0;

                        }
                        else if (transform.position.x == length - 1)
                        {
                            dir = 2;
                        }
                        else
                            dir = Random.Range(0, 4);
                    }

                    else
                    {
                        Destroy(GetRoom(transform.position));
                        if (genMethod == 2)
                        {
                            GenerateRoom(startingRooms[0]);
                        }
                        else
                        GenerateRoom(endRoom[0]);


                        genMap();
                        stageOneComp = true;
                        delay = 0;
                    }
                }
            }
            else
                delay++;
        }
        else
        {
            if (!stageTwoComp && delay >= 1)
            {
               // Debug.Log("please");
                for (int x = 0; x < length * scale; x++)
                {
                    for (int y = 0; y > -height * scale; y--)
                    {
                        Vector2 pos = new Vector2(x, y);
                        if (!tileDictionary.ContainsKey(pos) && HasGroundTile(pos))
                        {
                            //Debug.Log("checking spaces");
                            int count = CountAjacentTiles(pos);

                            if (count > 2 && Random.Range(0, 6) == 3)
                            {
                                //treasure
                                int idx = Random.Range(0, treasure.Length);
                                Instantiate(treasure[idx], pos, Quaternion.identity);
                               // Debug.Log("Treasure WHOOP WHOOP");
                            }
                            else if (count <= 3 && Random.Range(0, 7) == 0)
                            {   //enemies
                                int idx = Random.Range(0, enemies.Length);
                                Instantiate(enemies[idx], pos, Quaternion.identity);
                              //  Debug.Log("GAAAAASSSP..... THE ENEMY!!");
                            }
                            else if (count <= 4 && Random.Range(0, 4) == 0)
                            {
                                //trap
                                int idx = Random.Range(0, traps.Length);
                                Instantiate(traps[idx], pos, Quaternion.identity);
                              //  Debug.Log("ITS A TRAP!!");
                            }
                           
                        }
                    }
                }

                

                stageTwoComp = true;
                readyforplayer = true;

            }
            else if (!stageTwoComp)
            {
                delay++;
            }
            if (stageOneComp && stageTwoComp)
            {
                if (!pc)
                {
                    Instantiate(pathchecker);
                    pc = true;
                }

            }
        }
        
      
    }
    void MethodTwo()
    {
        if (!stageOneComp)
        {

            if (delay >= delayTime)
            {
                delay = 0;
                if (dir == 0 || dir == 1) // rooms moving right
                {
                    if (transform.position.x < length - 1)
                    {
                        transform.position += Vector3.right;
                        int r = 0;
                        GenerateRoom(paths[r]);
                        dir = Random.Range(0, 1);
                        
                       
                    }
            
        
                    else
                    {
                        Destroy(GetRoom(transform.position));
                        GenerateRoom(endRoom[0]);
                        genMap();
                        stageOneComp = true;
                        delay = 0;
                    }
                }
            }
            else
                delay++;
        }
    }
    void MethodThree()
    {

    }

    bool HasGroundTile(Vector2 pos)
    {
        return tileDictionary.ContainsKey(pos + Vector2.down);
    }

    int CountAjacentTiles(Vector2 pos)
    {
        int counter = 0;
        for(int x = -1; x< 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                Vector2 newPos = pos + new Vector2(x, y);
                if (newPos == pos)
                {
                    continue;
                }
                if (tileDictionary.ContainsKey(newPos))
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    void genMap()
    {
        // filler rooms

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < length; x++)
            {
                if (!loadedR.Contains(new Vector2(x, y)))
                {
                    int r = Random.Range(0, fillaRooms.Length);
                    transform.position = new Vector2(x, -y);
                    GenerateRoom(fillaRooms[r]);
                }
                
            }
        }
       
    }

    void GenerateRoom(GameObject room)
    {
        GameObject temp = Instantiate(room, transform.position * scale, Quaternion.identity);
        int x = (int)transform.position.x;
        int y = -(int)transform.position.y;

        roomArray[x, y] = temp;

        loadedR.Add(new Vector2(x,y));
    }

    GameObject GetRoom(Vector2 pos)
    {
        return roomArray[(int)pos.x, -(int)pos.y];
    }

    void InitGen()
    {
        if (rSeed)
        {
            seed = Random.Range(0, 1000000);
        }

        if (genMethod == 1)
        {
            height = 1;

        }
        else
            transform.position = new Vector2(Random.Range(0, length), 0);


        stageOneComp = false;
        stageTwoComp = false;
        roomArray = new GameObject[length, height];
        tileDictionary = new Dictionary<Vector2, GameObject>();  //instatiate blanmk dict
        Random.InitState(seed);

        if (genMethod == 2)
        {
            GenerateRoom(endRoom[0]);
        }
        else
            GenerateRoom(startingRooms[0]);

        if (transform.position.x == 0)
        {
            dir = 0;

        }
        else if (transform.position.x == length - 1)
        {
            dir = 2;
        }
        else
            dir = Random.Range(0, 4);
    }

   
}
