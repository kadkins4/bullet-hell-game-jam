using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] string color;
    [SerializeField] bool isActive = true;

    public string GetColor() {
        return color;
    }

    // public void SetColor(string newColor) {
    //     color = newColor;
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            FindObjectOfType<Player>().SetColor(color);
        }
    }
}
