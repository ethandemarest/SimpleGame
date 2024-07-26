using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector2 destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Teleport(collision.gameObject); 
    }

    void Teleport(GameObject player)
    {
        player.transform.position = destination;
    }
}
