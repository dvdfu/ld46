using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flips sprites depending on their horizontal velocity
public class SpriteFlipper : MonoBehaviour {
    [SerializeField]
    float threshold = 10.0f;
    [SerializeField]
    bool flipOtherWay = false;

    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Rigidbody2D body;

    // Update is called once per frame
    void Update() {
        if (body.velocity.x > threshold) {
            sprite.flipX = flipOtherWay == false;
        } else if (body.velocity.x < -threshold) {
            sprite.flipX = flipOtherWay == true;
        }
    }
}
