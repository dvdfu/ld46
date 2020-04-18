using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flips sprites depending on their horizontal velocity
public class SpriteFlipper : MonoBehaviour {
    [SerializeField]
    private float threshold = 10.0f;
    [SerializeField]
    private bool flipOtherWay = false;

    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Rigidbody2D body;

    // Update is called once per frame
    void Update() {
        if (body.velocity.x > threshold) {
            sprite.flipX = flipOtherWay == false;
        } else if (body.velocity.x < -threshold) {
            sprite.flipX = flipOtherWay == true;
        }
    }
}
