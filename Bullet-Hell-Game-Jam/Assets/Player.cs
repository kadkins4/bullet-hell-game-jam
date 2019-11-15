using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // TODO move playermovement items to a new player movement script to separate code
    [Header("Player Movement")]
    [SerializeField] [Range(0, 10)] float movementSpeed = 6f;
    Vector2 movement;

    [Header("Player Health")]
    [SerializeField] [Range(0, 3)] int playerHealth = 1;

    public Rigidbody2D rb;

    void Start() {
        // TODO Remove this debug line
        Debug.Log("---------GAME JAM TIME! BRING ON THE BULLETS!-----------");
    }

    void Update() {
        // handle input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        // this will prevent from framerate variation
        Move();
    }

    private void Move() {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);
    }
}
