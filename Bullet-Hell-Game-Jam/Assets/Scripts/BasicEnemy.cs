using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public enum Color
{
    Red,
    Yellow,
    Blue
}

public class BasicEnemy : MonoBehaviour
{

    public int size;

    public float speed;

    Color enemyColor;

    private GameObject player;
    private Rigidbody2D rigidbody;
    private CircleCollider2D collider;

    private SpriteRenderer renderer;
    private Transform childTransform;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        collider = GetComponent<CircleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        enemyColor = (Color)Random.Range(0, 2);

        renderer = GetComponentInChildren<SpriteRenderer>();
        childTransform = transform.GetChild(0);

        size = Random.Range(1, 10);

        collider.radius = size * 0.25f;
        childTransform.localScale = Vector2.one * size * 3f;

        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector2 lookAtPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        transform.up = lookAtPoint - currentPosition;

        rigidbody.velocity = new Vector2(transform.up.x, transform.up.y) * speed;
    }

    void UpdateSize()
    {
        collider.radius = size * 0.25f;
        childTransform.localScale = Vector2.one * size * 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateColor()
    {
        switch(enemyColor)
        {
            case Color.Red:
                break;
            case Color.Yellow:
                break;
            case Color.Blue:
                break;
        }
    }

    //For when size changes in editor
    private void OnValidate()
    {
        UpdateSize();
    }
}
