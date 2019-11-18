using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEvent : UnityEvent<GameObject>
{

}



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    [Header("Starting Speed")]
    public float speed;
   

    public GameObjectEvent deathEvent = new GameObjectEvent();

    public Rigidbody2D _rigidbody;
    public CircleCollider2D _collider;

    public Animator enemyAnimator;

    public virtual void Start()
    {

        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        if(enemyAnimator == null)
        {
            enemyAnimator = GetComponent<Animator>();
        }
    }

    public void OnDeath()
    {
        //Calls unity event
        deathEvent.Invoke(gameObject);
    }

}



public class BasicEnemy : Enemy
{

    [Header("The enemy spawned by death")]
    public GameObject enemySpawn;

    private int spawnCount;

    public EnemySpawner enemySpawner;





    public int maxSize = 4;
    [Range(1, 10)] public int size;

    [Header("Dev Purposes, Change color here manually")]
    public Colors enemyColor;

    private GameObject player;
    private Transform playerTransform;

    public SpriteRenderer _renderer;
    public Transform childTransform;

    // Start is called before the first frame update
    override public void Start()
    {
        spawnCount = Random.Range(2, 3);
        enemySpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();

        player = GameObject.FindGameObjectWithTag("Player");

        childTransform = transform.GetChild(0);

        if (_renderer == null)
        {
            _renderer = childTransform.GetComponent<SpriteRenderer>();
        }
        

        size = Random.Range(1, maxSize);
        UpdateSize();

        MoveTowardsPlayer();

        enemyColor = (Colors)Random.Range(0, 4);
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
        if(collision.transform.tag == "Wall")
        {
            MoveTowardsPlayer();
        }
        if(collision.transform.tag == "Projectile")
        {
            if(collision.transform.GetComponent<Projectile>().colorState == enemyColor)
            {
                enemyAnimator.SetTrigger("Hit");
                StartCoroutine(hitHue(0.15f));
                size -= 1;
                if(size == 0)
                {
                    OnDeath();
                }
                
                UpdateSize();
            } 
            else 
            {
                if (size < maxSize) // TODO make the 4 a variable so we aren't hard-coding
                {
                    size += 1;
                    UpdateSize();
                }
                else
                {
                    Explode();
                }
            }
        }
    }

    void UpdateSize()
    {
        _collider.radius = size * 0.35f;
        enemyAnimator.SetInteger("Size", size);
    }

    // Update is called once per frame
    void Update()
    {
        //Death
        if(size == 0)
        {
            OnDeath();
        }

        childTransform.rotation = Quaternion.Euler(Vector2.zero);
    }

    void UpdateColor()
    {
        switch(enemyColor)
        {
            case Colors.Red:
                enemyAnimator.SetInteger("Color", 0);
                break;
            case Colors.Yellow:
                enemyAnimator.SetInteger("Color", 1);
                break;
            case Colors.Blue:
                enemyAnimator.SetInteger("Color", 2);
                break;
            case Colors.Green:
                enemyAnimator.SetInteger("Color", 3);
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

    IEnumerator hitHue(float duration)
    {
        float elapsed = 0.0f;
        

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _renderer.color = Color.Lerp(Color.white, Color.red, elapsed / duration);
            yield return null;
        }

        _renderer.color = Color.white;

    }

    void Explode()
    {
        for (int i = 1; i <= spawnCount; i++)
        {
            enemySpawner.InstantiateEnemy(enemySpawn, transform.position);
        }
        OnDeath();
    }

}