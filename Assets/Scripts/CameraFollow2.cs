using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    PlayerController pc;
    Rigidbody2D rb;
    public Camera cameraMain;
    public GameObject player;

    Vector3 cameraAngle;

    Vector2 playerPos;
    Vector3 velVec = Vector2.zero;
    Vector2 tempPosition;
    Vector2 offsetY = new Vector3(0f, 5f);
    Vector2 lookAhead;
    private Vector2 playerVel;
    public Vector2 playerInfluence;

    public float lookAheadFactor = 3f;
    public float smoothTime = 0.5f;
    public float xVelFactor;
    public float yVelFactor;

    float orthoSize;
    float tempOrthoSize;
    public float zOffset;
    private float influenceFactor = 1f;

    public float defaultZoom = 8f;

    private float vel = 0.0f;
    public bool followPlayer = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraMain = transform.GetChild(0).GetComponent<Camera>();
    }
    private void Update()
    {
        //POSITIONS;
        Vector2 playerCam = playerPos + offsetY + playerVel + lookAhead;
        Vector2 cameraPos = tempPosition + playerInfluence;

        cameraAngle = followPlayer ? playerCam : cameraPos;
        orthoSize = followPlayer ? defaultZoom : tempOrthoSize;

        if (player)
        {
            pc = player.GetComponent<PlayerController>();
            rb = player.GetComponent<Rigidbody2D>();

            playerVel = new Vector3(rb.velocity.x * xVelFactor, rb.velocity.y * yVelFactor);

            playerInfluence.x = (player.transform.position.x - tempPosition.x) / influenceFactor;
            playerInfluence.y = (player.transform.position.y - tempPosition.y) / influenceFactor;

            lookAhead = new Vector3(pc.lastMove,0) * lookAheadFactor;

            playerPos = player.transform.position;
        }
        if (!player)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }

    private void FixedUpdate()
    {
        //POSITION
        Vector3 newAngle = new Vector3(cameraAngle.x, cameraAngle.y, zOffset);
        transform.position = Vector3.SmoothDamp(transform.position, newAngle, ref velVec, smoothTime);

        //ZOOM
        cameraMain.orthographicSize = Mathf.SmoothDamp(cameraMain.orthographicSize, orthoSize, ref vel, smoothTime);
    }

    public void NewAngle(Vector2 angle, float orthoZoom, float influence)
    {
        followPlayer = false;
        tempPosition = angle;
        influenceFactor = influence;
        tempOrthoSize = orthoZoom;
    }
    public void AngleReset()
    {
        followPlayer = true;
    }
}