using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    bool inWind;
    public Vector2 windDirection;
    Vector2 tempWind;

    public float windUp;
    public float windDown;

    void Update()
    {
        if (inWind)
        {
            windDirection = Vector2.MoveTowards(windDirection, tempWind, windUp);
        }
        else if (!inWind)
        {
            windDirection = Vector2.MoveTowards(windDirection, Vector2.zero, windDown);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            inWind = true;
            tempWind = collision.GetComponent<WindZone>().windDirection * collision.GetComponent<WindZone>().windSpeed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            inWind = false;

        }
    }
}
