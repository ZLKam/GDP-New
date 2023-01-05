using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject fixedJoystick;
    private FixedJoystick joystick;

    Rigidbody2D rb2D;
    Animator animator;

    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        joystick = fixedJoystick.GetComponent<FixedJoystick>();

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacterUsingJoystick();
        CharacterAnimation();
    }

    private void MoveCharacterUsingJoystick()
    {
        rb2D.velocity = new Vector2(joystick.Horizontal * moveSpeed, joystick.Vertical * moveSpeed);
    }

    private void CharacterAnimation()
    {
        if (rb2D.velocity != Vector2.zero)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if (rb2D.velocity.x < 0)
        {
            transform.localScale = new(-1, 1, 1);
        }
        else if (rb2D.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }
}
