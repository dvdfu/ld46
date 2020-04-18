using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Sprite normalHouse;
    [SerializeField]
    Sprite burnedHouse;
    [SerializeField]
    Sprite ashes;

    [SerializeField]
    Mortal mortal;

    [SerializeField]
    GameObject fire;
    [SerializeField]
    bool setOnFire = false;

    void Update() {
        if (setOnFire) {
            setOnFire = false;
            Instantiate(fire, transform.position + Vector3.up * 16.0f, Quaternion.identity);
        }

        if (mortal.GetAlive()) {
            if (mortal.GetHealth() >= 50.0f) {
                sprite.sprite = normalHouse;
            } else {
                sprite.sprite = burnedHouse;
            }
        }
    }

    public void Die() {
        sprite.sprite = ashes;
    }
}
