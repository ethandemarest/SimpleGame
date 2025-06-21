using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    GameObject player;
    public GameObject[] platforms;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        platforms = GameObject.FindGameObjectsWithTag("Ground");


        foreach (GameObject gameObject in platforms)
        {
            float dist = Vector2.Distance(player.transform.position, transform.position);

            if(gameObject.transform.position.x >= (transform.position.x - dist))
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
