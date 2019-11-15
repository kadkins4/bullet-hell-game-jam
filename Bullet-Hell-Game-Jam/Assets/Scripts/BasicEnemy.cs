using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Colors
{
    Red,
    Yellow,
    Blue
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BasicEnemy : MonoBehaviour
{

    public int size;

    [Header("Starting Speed")]
    public float speed;

    [Header("")]
    public Colors enemyColor;

    private GameObject player;
    private Rigidbody2D _rigidbody;
    public CircleCollider2D _collider;

    private SpriteRenderer _renderer;
    public Transform childTransform;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();

        _renderer = childTransform.GetComponent<SpriteRenderer>();
        childTransform = transform.GetChild(0);

        size = Random.Range(1, 10);
        UpdateSize();

        MoveTowardsPlayer();

        enemyColor = (Colors)Random.Range(0, 2);
        UpdateColor();
    }

    void MoveTowardsPlayer()
    {
        Vector2 lookAtPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        transform.up = lookAtPoint - currentPosition;

        _rigidbody.velocity = new Vector2(transform.up.x, transform.up.y) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Bounds")
        {
            MoveTowardsPlayer();
        }
    }

    void UpdateSize()
    {
        _collider.radius = size * 0.25f;
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
            case Colors.Red:
                _renderer.color = Color.red;
                break;
            case Colors.Yellow:
                _renderer.color = Color.yellow;
                break;
            case Colors.Blue:
                _renderer.color = Color.blue;
                break;
            default:
                break;
        }
    }

    //For when size changes in editor
    private void OnValidate()
    {
        UpdateSize();
        UpdateColor();
    }
}
