using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour
{
    void Start()
    {
        float zIncrease = -transform.position.z * 5;
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(zIncrease);
    }
}
