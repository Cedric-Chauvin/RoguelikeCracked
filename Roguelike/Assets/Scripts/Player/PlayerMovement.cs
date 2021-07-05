using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;

    public Joystick joystick;
    private PlayerManager P_Manager;

    Vector2 movement;

    private void Start()
    {
        P_Manager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = P_Manager.PlayerSpeed;
        // Input
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
