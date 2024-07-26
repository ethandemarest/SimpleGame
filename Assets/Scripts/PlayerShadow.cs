using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    LayerMask GroundMask;
    GameObject player;
    public Vector3 offset;
    SpriteRenderer sp;
    float scaleFactor;

    // Start is called before the first frame update
    void Start()
    {
        GroundMask = LayerMask.GetMask("Ground");
        player = transform.parent.gameObject;
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 20f, GroundMask);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - hit.distance) + offset;

        if (hit)
        {
            scaleFactor = (hit.distance / 20);
        }
        else if (!hit)
        {
            scaleFactor = 50;
        }
        float newScale = Mathf.Clamp(1f - scaleFactor, 0f, 1f);

        sp.color = new Color(0f, 0f, 0f, 1f - scaleFactor);

        transform.localScale = new Vector3(newScale, newScale, 1);


    }
}
