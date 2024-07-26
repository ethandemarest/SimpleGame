using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionDebug : MonoBehaviour
{
    SpriteRenderer sp;
    PlayerController pc;

    public Color idle;
    public Color run;
    public Color jump;
    public Color fall;
    public Color dash;
    public Color wallGrab;
    
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        pc = GetComponent<PlayerController>();
        SetColor(idle);
    }

    public void SetColor(Color newColor)
    {
        sp.color = newColor;
    }
    private void Update()
    {
        if (pc.dashing)
        {
            SetColor(dash);
        }
        else
        {
            SetColor(idle);
        }

        if (pc.grounded)
        {

        }
    }
}
