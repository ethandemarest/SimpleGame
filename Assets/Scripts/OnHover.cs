using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class OnHover : MonoBehaviour
{
    private void OnMouseOver()
    {
        print("hovering");
        GetComponent<UnityEngine.UI.Button>().Select();
    }
}
