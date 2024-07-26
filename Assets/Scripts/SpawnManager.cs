using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;

    public void SpawnPlayer(Vector2 spawnLocation)
    {
        Instantiate(player, spawnLocation, Quaternion.Euler(Vector2.zero));
    }
}
