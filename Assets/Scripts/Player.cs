using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Consts
    [SerializeField]
    private Vector2 MAX_SPEED = new Vector2(5.0f, 5.0f);
    [SerializeField]
    private float ACCELERATION_FACTOR = 0.5f;

    // Member vars
    private Vector2 speed = Vector2.zero;

    // Unity vars
    [SerializeField]
    private Rigidbody2D body;

    void Start() {
        
    }

    void Update() {
        // TODO: use forces?
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        speed += ((moveDirection * MAX_SPEED) - speed) * ACCELERATION_FACTOR;
        body.position = new Vector2(transform.position.x + speed.x * Time.deltaTime, transform.position.y + speed.y * Time.deltaTime);
    }
}
