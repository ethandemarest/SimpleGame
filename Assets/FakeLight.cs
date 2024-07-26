using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLight : MonoBehaviour
{
    LayerMask GroundMask;
    public GameObject lightBeam;
    public Vector2 offset; 
    GameObject newBeam;

    public Color lightColor;

    // Start is called before the first frame update
    void Start()
    {
        GroundMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 50f, GroundMask);
        var hitRotation = Quaternion.LookRotation(hit.normal, Vector3.up);
        newBeam = Instantiate(lightBeam, hit.point + offset, hitRotation);
        newBeam.GetComponent<SpriteRenderer>().color = lightColor;
        newBeam.transform.localScale = new Vector3(hit.distance/10, 0.4f, 0.4f);
    }
}
    