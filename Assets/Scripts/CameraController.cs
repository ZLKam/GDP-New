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

    private float halfWidth;
    private float halfHeight;

    private Vector2 cameraLeftBorder;
    private Vector2 cameraRightBorder;
    private Vector2 cameraTopBorder;
    private Vector2 cameraBottomBorder;

    // Start is called before the first frame update
    void Start()
    {
        backgroundLeftBorder = background.transform.Find("Left").transform;
        backgroundRightBorder = background.transform.Find("Right").transform;
        backgroundTopBorder = background.transform.Find("Top").transform;
        backgroundBottomBorder = background.transform.Find("Bottom").transform;

        transform.position = player.transform.position + cameraOffset;

        halfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        halfHeight = Camera.main.orthographicSize;

        //cameraLeftBorder = new(transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height, transform.position.y);
        //cameraRightBorder = new(transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height, transform.position.y);
        //cameraTopBorder = new(transform.position.x, transform.position.y + Camera.main.orthographicSize);
        //cameraBottomBorder = new(transform.position.x, transform.position.y - Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new(player.transform.position.x, player.transform.position.y, -10f);

        CheckBorder();
    }

    private void CheckBorder()
    {
        RaycastHit2D leftCheck = Physics2D.Raycast(transform.position, Vector2.left, halfWidth, 11 << LayerMask.NameToLayer("Tasks"));
        RaycastHit2D rightCheck = Physics2D.Raycast(transform.position, Vector2.right, halfWidth);
        RaycastHit2D topCheck = Physics2D.Raycast(transform.position, Vector2.up, halfHeight);
        RaycastHit2D bottomCheck = Physics2D.Raycast(transform.position, Vector2.down, halfHeight);

        Debug.DrawRay(transform.position, Vector2.right * halfWidth);

        if (leftCheck.collider != null && leftCheck.collider.name == backgroundLeftBorder.name)
        {
            transform.position = leftCheck.transform.position + new Vector3(halfWidth, transform.position.y, 0) + cameraOffset;
        }
        if (rightCheck.collider != null && rightCheck.collider.name == backgroundRightBorder.name)
        {
            transform.position = rightCheck.transform.position + new Vector3(-halfWidth, transform.position.y, 0) + cameraOffset;
        }
        if (topCheck.collider != null && topCheck.collider.name == backgroundTopBorder.name)
        {
            transform.position = topCheck.transform.position + new Vector3(transform.position.x, -halfHeight, 0) + cameraOffset;
        }
        if (bottomCheck.collider != null && bottomCheck.collider.name == backgroundBottomBorder.name)
        {
            transform.position = bottomCheck.transform.position + new Vector3(transform.position.x, halfHeight, 0) + cameraOffset;
        }
        //if (cameraLeftBorder.x <= backgroundLeftBorder.transform.position.x ||
        //    cameraRightBorder.x >= backgroundRightBorder.transform.position.x)
        //{
            
        //    transform.position = new Vector3(cameraPreviousPosition.x, transform.position.y, -10f);
        //}
        //if (cameraTopBorder.y >= backgroundTopBorder.transform.position.y ||
        //    cameraBottomBorder.y <= backgroundBottomBorder.transform.position.y)
        //{
        //    transform.position = new Vector3(transform.position.x, cameraPreviousPosition.y, -10f);
        //}
        //cameraPreviousPosition = transform.position;
    }
}
