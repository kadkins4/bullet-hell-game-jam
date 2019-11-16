using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // TODO move playermovement items to a new player movement script to separate code -- or do we need to do this?
    [Header("Player Movement")]
    [SerializeField] [Range(0, 10)] float movementSpeed = 6f;
    Vector2 _movement;
    Vector3 _mousePosition;

    // TODO possible feature -- could add a dash or a blink (like Tracer) that slowly regenerates?! idk

    [Header("Player Health")]
    [SerializeField] [Range(0, 3)] int playerHealth = 1;

    [Header("Player Shooting")]
    [SerializeField] GameObject firePoint, projectile;

    Color _activeProjectileColor;
    [SerializeField] Color yellowProj, redProj, greenProj, blueProj;

    [Header("General")]
    [SerializeField] Rigidbody2D rb; // TODO instead of public -- get rb on Awake()
    [SerializeField] SpriteRenderer activeRend;
    [SerializeField] Sprite redPlayer;
    [SerializeField] Sprite greenPlayer;
    [SerializeField] Sprite yellowPlayer;
    [SerializeField] Sprite bluePlayer;

    void Start() {
        // TODO Remove this debug line
        Debug.Log("---------GAME JAM TIME! BRING ON THE BULLETS!-----------");
        SetColor("Red");
    }

    void Update() {
        // handle input
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        // find mouse position
        _mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        if (Input.GetButtonDown("Fire1")) { // using fire1 so that we have the flexibility to change the hotkey later
            _Shoot();
        }
    }

    void FixedUpdate() {
        // this will prevent from framerate variation
        _Move();
        _RotatePlayer();
    }

    private void _Move() {
        Vector2 _velocity = _movement.normalized * movementSpeed;
        rb.MovePosition(rb.position + _velocity * Time.deltaTime);
    }

    private void _RotatePlayer() {
        // find the amount of rotation needed to face mouse position
        var angle = Mathf.Atan2(_mousePosition.x, _mousePosition.y) * Mathf.Rad2Deg;
        // add amount needed to current rotation.
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back); // @NOTE Not sure why I needed to use back instead of forward -- shrug
    }

    private void _Shoot() {
        GameObject bullet = Instantiate(
            projectile,
            firePoint.transform.position,
            Quaternion.identity
        ) as GameObject;
    }

    public Vector3 GetMousePosition() {
        return _mousePosition;
    }

    public void SetColor(string color) {
        if (color == "Blue") {
            activeRend.sprite = bluePlayer;
            _activeProjectileColor = blueProj;
        }

        if (color == "Yellow") {
            activeRend.sprite = yellowPlayer;
            _activeProjectileColor = yellowProj;
        }

        if (color == "Red") {
            activeRend.sprite = redPlayer;
            _activeProjectileColor = redProj;
        }

        if (color == "Green") {
            activeRend.sprite = greenPlayer;
            _activeProjectileColor = greenProj;
        }

    }

    public Color GetColor() {
        return _activeProjectileColor;
    }
}
