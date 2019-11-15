using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] float thrust = 2f;
    public Rigidbody2D rb;

    Vector3 _mousePosition;
    Color _color;

    void Awake() {
        _mousePosition = FindObjectOfType<Player>().GetMousePosition();
        _color = FindObjectOfType<Player>().GetColor();
    }

    // Start is called before the first frame update
    void Start()
    {
        _RotateBullet();
        _AccelerateBullet();
    }

    private void _RotateBullet() {
        // find the amount of rotation needed to face mouse position
        var angle = Mathf.Atan2(_mousePosition.x, _mousePosition.y) * Mathf.Rad2Deg;
        // add amount needed to current rotation
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
    }

    private void _AccelerateBullet() {
        float actualThrust = thrust * 10f;
        rb.AddForce(transform.up * actualThrust, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var tag = other.gameObject.tag;

        if (other.gameObject.tag != "Player") { // ignore player
            Debug.Log(tag);
            if (tag == "Wall") {
                Destroy(gameObject);
            }

            if (tag == "Enemy") {
                // if correct color
                    // deal dmg
                // else
                    // side effect
            }
        }
    }
}
