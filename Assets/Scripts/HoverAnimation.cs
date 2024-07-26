using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    SpriteRenderer sp;
    public float speedUpDown = 1;
    public float distanceUpDown = 1;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        toggleVis(1);
    }
    private void Update()
    {
        float mov = Mathf.Sin(speedUpDown * Time.time) * distanceUpDown;
        transform.position = new Vector3(transform.position.x, transform.position.y + mov, 0.0f);
    }

    public void toggleVis(int colorMode)
    {
        Color[] col = new Color[2];
        col[0] = new Color(1f, 1f, 1f, 1f);
        col[1] = new Color(1f, 1f, 1f, 0f);

        sp.color = col[colorMode];
    }
}
 