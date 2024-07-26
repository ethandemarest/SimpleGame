using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNewAnge : MonoBehaviour
{
    GameObject cameraMain;

    [Header("New Angle:")]
    public Vector2 cameraPosition;

    [Header("Player's Influence On Camera")]
    public float playerInfluence = 3;

    [Header("Ortho Size  (smaller is closer)")]
    public float zoomLevel = 3;

    void Start()
    {
        cameraMain = GameObject.Find("Camera Holder");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraMain.GetComponent<CameraFollow2>().NewAngle(cameraPosition, zoomLevel, playerInfluence);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraMain.GetComponent<CameraFollow2>().AngleReset();  

        }
    }
}
