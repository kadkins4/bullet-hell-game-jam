using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryhardEnemy : Enemy
{

    private GameObject player;
    [Header("Size, shouldnt change in gameplay")][Range(1, 4)]
    public int size;

    public Transform childTransform;

    override public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        childTransform = transform.GetChild(0);
    }

    void MoveTowardsPlayer()
    {
        Vector2 lookAtPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        transform.up = lookAtPoint - currentPosition;

        _rigidbody.velocity = new Vector2(transform.up.x, transform.up.y) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Probably inefficient but who cares
        MoveTowardsPlayer();
        childTransform.rotation = Quaternion.Euler(Vector2.zero);
    }

    void UpdateSize()
    {
        _collider.radius = size * 0.75f;
        childTransform.localScale = Vector2.one * size * 3f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.transform.tag == "Projectile")
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        OnDeath();
    }

    private void OnValidate()
    {
        UpdateSize();
    }

}