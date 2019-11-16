﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Colors
{
    Red,
    Yellow,
    Blue
}

public class GameObjectEvent : UnityEvent<GameObject>
{

}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    [Header("Starting Speed")]
    public float speed;

    public Rigidbody2D _rigidbody;
    public CircleCollider2D _collider;

    public virtual void Start()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}



public class BasicEnemy : Enemy
{

    public GameObjectEvent deathEvent = new GameObjectEvent();

    public int size;

    [Header("Dev Purposes, Change color here manually")]
    public Colors enemyColor;

    private GameObject player;
    private Transform playerTransform;

    public SpriteRenderer _renderer;
    public Transform childTransform;


    // Start is called before the first frame update
    override public void Start()
    { 

        player = GameObject.FindGameObjectWithTag("Player");
        

        if(_renderer == null)
        {
            _renderer = childTransform.GetComponent<SpriteRenderer>();
        }
        childTransform = transform.GetChild(0);

        size = Random.Range(1, 4);
        UpdateSize();

        MoveTowardsPlayer();

        enemyColor = (Colors)Random.Range(0, 3);
        UpdateColor();
    }

    void MoveTowardsPlayer()
    {
        //Ignore this error, everything works fine
        Vector2 lookAtPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        transform.up = lookAtPoint - currentPosition;

        _rigidbody.velocity = new Vector2(transform.up.x, transform.up.y) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If map edge hit, reorient towards player
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
        //Death
        if(size == 0)
        {
            OnDeath();
        }
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

    private void OnDeath()
    {
        //Calls unity event
        deathEvent.Invoke(gameObject);
    }

}
