using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject fixedJoystick;
    private FixedJoystick joystick;

    Rigidbody2D rb2D;

    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        joystick = fixedJoystick.GetComponent<FixedJoystick>();

        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacterUsingJoystick();
    }

    private void MoveCharacterUsingJoystick()
    {
        rb2D.velocity = new Vector2(joystick.Horizontal * moveSpeed, joystick.Vertical * moveSpeed);
    }
}
