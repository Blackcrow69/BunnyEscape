using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] clouds;

    private float distanceBetweenClouds = 3f;
    private float minX;
    private float maxX;
    private float lastCloudPosY;
    private float controlX;

    [SerializeField]
    private GameObject[] collectables;

    private GameObject player;

    private void Awake()
    {
        controlX = 0f;
        SetMinMaxX();
        CreateClouds();
        player = GameObject.Find("Player");

        for (int i = 0; i < collectables.Length; i++)
        {
            collectables[i].SetActive(false);
        }
    }

    private void Start()
    {
        PositionPlayer();
    }


    void SetMinMaxX()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        maxX = bounds.x - 0.5f;
        minX = -bounds.x + 0.5f;
    }

    void Shuffle(GameObject[] arrayToShuffle)
    {
        for (int i = 0; i < arrayToShuffle.Length; i++)
        {
            GameObject temp = arrayToShuffle[i];
            int random = Random.Range(i, arrayToShuffle.Length);
            arrayToShuffle[i] = arrayToShuffle[random];
            arrayToShuffle[random] = temp;
        }
    }

    void CreateClouds()
    {
        Shuffle(clouds);

        float positionY = 0f;
        
        for (int i = 0; i < clouds.Length; i++)
        {
            Vector3 temp = clouds[i].transform.position;
            temp.y = positionY;
            
            if (controlX == 0)
            {
                temp.x = Random.Range(0f, maxX);
                controlX = 1f;
            }
            else if (controlX == 1f)
            {
                temp.x = Random.Range(0f, minX);
                controlX = 2f;
             }
            else if (controlX == 2f)
            {
                temp.x = Random.Range(1f, maxX);
                controlX = 3f;
            }
            else if (controlX == 3f)
            {
                temp.x = Random.Range(-1f, minX);
                controlX = 0f;
            }

            lastCloudPosY = positionY;
            clouds[i].transform.position = temp;
            positionY -= distanceBetweenClouds;
        }
    }

    void PositionPlayer()
    {
        GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
        GameObject[] CloudsInGame = GameObject.FindGameObjectsWithTag("Cloud");

        for (int i = 0; i < darkClouds.Length; i++)
        {
            if (darkClouds[i].transform.position.y == 0f)
            {
                Vector3 t = darkClouds[i].transform.position = new Vector3(CloudsInGame[0].transform.position.x,
                                                                            CloudsInGame[0].transform.position.y,
                                                                            CloudsInGame[0].transform.position.z);
                CloudsInGame[0].transform.position = t;
            }
        }

        Vector3 temp = CloudsInGame[0].transform.position;
        for (int i = 1; i < CloudsInGame.Length; i++)
        {
            if (temp.y < CloudsInGame[i].transform.position.y)
            {
                temp = CloudsInGame[i].transform.position;
            }
        }

        // positioning the player above the cloud
        player.transform.position = new Vector3(temp.x, temp.y + 0.8f, temp.z);
    }



    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Cloud" || target.tag == "Deadly")
        {
            if (target.transform.position.y == lastCloudPosY)
            {
                Shuffle(clouds);
                Shuffle(collectables);

                Vector3 temp = target.transform.position;

                for (int i = 0; i < clouds.Length; i++)
                {
                    if (!clouds[i].activeInHierarchy)
                    {
                        if (controlX == 0)
                        {
                            temp.x = Random.Range(0f, maxX);
                            controlX = 1f;
                        }
                        else if (controlX == 1f)
                        {
                            temp.x = Random.Range(0f, minX);
                            controlX = 2f;
                        }
                        else if (controlX == 2f)
                        {
                            temp.x = Random.Range(1f, maxX);
                            controlX = 3f;
                        }
                        else if (controlX == 3f)
                        {
                            temp.x = Random.Range(-1f, minX);
                            controlX = 0f;
                        }

                        temp.y -= distanceBetweenClouds;
                        lastCloudPosY = temp.y;
                        clouds[i].transform.position = temp;
                        clouds[i].SetActive(true);

                        int random = Random.Range(0, collectables.Length);
                        if (clouds[i].tag != "Deadly")
                        {
                            if (!collectables[random].activeInHierarchy)
                            {
                                Vector3 temp2 = clouds[i].transform.position;
                                temp2.y += 0.7f;

                                if (collectables[random].tag == "Life")
                                {
                                    if (PlayerScore.lifeCount < 2)
                                    {
                                        collectables[random].transform.position = temp2;
                                        collectables[random].SetActive(true);
                                    }
                                }
                                else
                                {
                                    collectables[random].transform.position = temp2;
                                    collectables[random].SetActive(true);
                                }
                            }
                        }

                    }
                }


            }
        }
    }

}
