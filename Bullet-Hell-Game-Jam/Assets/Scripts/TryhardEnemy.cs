using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryhardEnemy : Enemy
{

    private GameObject player;

    override public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
    }
}
