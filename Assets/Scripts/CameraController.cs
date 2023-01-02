using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Background")]
    public GameObject background;

    private Transform backgroundLeftBorder;
    private Transform backgroundRightBorder;
    private Transform backgroundTopBorder;
    private Transform backgroundBottomBorder;

    [Header("Player")]
    public GameObject player;

    private Vector3 cameraPreviousPosition;
    private Vector3 cameraOffset = new(0, 0, -10f);

    // Start is called before the first frame update
    void Start()
    {
        backgroundLeftBorder = background.transform.Find("Left").transform;
        backgroundRightBorder = background.transform.Find("Right").transform;
        backgroundTopBorder = background.transform.Find("Top").transform;
        backgroundBottomBorder = background.transform.Find("Bottom").transform;

        transform.position = player.transform.position + cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new(player.transform.position.x, player.transform.position.y, -10f);

        CheckBorder();
    }

    private void CheckBorder()
    {
        Vector2 cameraLeftBorder = new(transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height, transform.position.y);
        Vector2 cameraRightBorder = new(transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height, transform.position.y);
        Vector2 cameraTopBorder = new(transform.position.x, transform.position.y + Camera.main.orthographicSize);
        Vector2 cameraBottomBorder = new(transform.position.x, transform.position.y - Camera.main.orthographicSize);
        if (cameraLeftBorder.x <= backgroundLeftBorder.transform.position.x ||
            cameraRightBorder.x >= backgroundRightBorder.transform.position.x)
        {
            transform.position = new Vector3(cameraPreviousPosition.x, transform.position.y, -10f);
        }
        if (cameraTopBorder.y >= backgroundTopBorder.transform.position.y ||
            cameraBottomBorder.y <= backgroundBottomBorder.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, cameraPreviousPosition.y, -10f);
        }
        cameraPreviousPosition = transform.position;
    }
}
